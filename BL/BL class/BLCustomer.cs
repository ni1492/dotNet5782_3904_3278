using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;


namespace BlApi
{
    public partial class BL : IBL
    {
        public void addCustomer(customer customer) //adding a new customer
        {
            try 
            {
                dl.AddCustomer(customer.id, customer.name, customer.phone, customer.location.Longitude, customer.location.Latitude);
            }
            catch (Exception ex) //catches if the ID already exists
            {
                throw new BO.exceptions.ExistException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }

        }
        public void updateCustomer(int id, string name, string phone) //update a customers details
        {
            try
            {
                DO.Customer tempDL = dl.DisplayCustomers(customer => customer.Id == id).First();

                dl.deleteCustomer(id); 
                if (name != null)
                    tempDL.Name = name;
                if (phone != null)
                    tempDL.Phone = phone;

                dl.AddCustomer(tempDL.Id, tempDL.Name, tempDL.Phone, tempDL.Longitude, tempDL.Lattitude);
            }
            catch (Exception ex) //throw - if the customer doesnt exist
            {

                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }

        }
        public customer displayCustomer(int id) //displays the customer requested
        {
            try
            {
                DO.Customer cus = dl.DisplayCustomers(customer => customer.Id == id).First();
                List<parcelAtCustomer> fromCus = new();
                List<parcelAtCustomer> toCus = new();
                int sId = 0;
                int tId = 0;
                foreach (var p in displayParcelList())
                {
                    foreach (var item in dl.DisplayCustomers(customer => true)) 
                    {
                        if (item.Name == p.sender)
                            sId = item.Id;
                        if (item.Name == p.receiver)
                            tId = item.Id;
                    }
                    if (p.receiver == cus.Name)
                    {
                        toCus.Add(new parcelAtCustomer
                        {
                            id = p.id,
                            weight = p.weight,
                            priority = p.priority,
                            status = getStatus(p.id), 
                            otherCus = new customerForParcel
                            {
                                name = p.sender,
                                id = sId
                            }
                        });
                    }
                    else if (p.sender == cus.Name)
                    {
                        fromCus.Add(new parcelAtCustomer
                        {
                            id = p.id,
                            weight = p.weight,
                            priority = p.priority,
                            status = getStatus(p.id),
                            otherCus = new customerForParcel
                            {
                                name = p.receiver,
                                id = tId
                            }
                        });
                    }
                }
                return new customer
                {
                    id = cus.Id,
                    name = cus.Name,
                    phone = cus.Phone,
                    location = new location
                    {
                        Latitude = cus.Lattitude,
                        Longitude = cus.Longitude
                    },
                    fromCus = fromCus,
                    toCus = toCus
                };
            }
            catch (Exception ex) //throw - if the customer doesnt exist
            {

                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }

        }

        public IEnumerable<customerForList> displayCustomerList() //displays the list of customers
        {
            foreach (var cus in dl.DisplayCustomers(customer=>true))
            {
                int notAcceptedPar = 0;
                int notDeliveredPar = 0;
                customer c = displayCustomer(cus.Id);
                foreach (var parcel in c.toCus)
                {
                    if (parcel.status != ParcelStatus.Delivered)
                        notAcceptedPar++;
                }
                foreach (var parcel in c.fromCus)
                {
                    if (parcel.status != ParcelStatus.Delivered)
                        notDeliveredPar++;
                }
                yield return (new customerForList
                {
                    id = cus.Id,
                    name = cus.Name,
                    phone = cus.Phone,
                    notDeliveredPar = notDeliveredPar,
                    deliveredPar = c.fromCus.Count() - notDeliveredPar,
                    notAcceptedPar = notAcceptedPar,
                    acceptedPar = c.toCus.Count() - notAcceptedPar

                });
            }
        }

    }
}

