using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;


namespace IBL
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
                throw new ExistException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }

        }
        public void updateCustomer(int id, string name, string phone) //update a customers details
        {
            IDAL.DO.Customer tempDL = dl.PrintCustomer(id); //catch

            dl.deleteCustomer(id);  //catch
            if (name != null) 
                tempDL.Name = name;
            if (phone != null)
                tempDL.Phone = phone;

            dl.AddCustomer(tempDL.Id, tempDL.Name, tempDL.Phone, tempDL.Longitude, tempDL.Lattitude); //catch 
            //throw - if the customer doesnt exist
        }
        public customer displayCustomer(int id) //displays the customer requested
        {
            IDAL.DO.Customer cus = dl.PrintCustomer(id); //catch
            List<parcelAtCustomer> fromCus = new();
            List<parcelAtCustomer> toCus = new();
            int sId = 0;
            int tId = 0;
            foreach (var p in displayParcelList())
            {
                foreach (var item in dl.PrintAllCustomer())
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
                        status = getStatus(p.id), //catch
                        otherCus = new customerForParcel
                        {
                            name = p.sender,
                            id=sId
 //                           id = dl.PrintAllCustomer().ToList().Find(customer => customer.Name == p.sender).Id

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
                            //   id = dl.PrintAllCustomer().ToList().Find(customer => customer.Name == p.receiver).Id

                        }
                    }) ;
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
            //throw if the customer doesnt exist
        }

        public IEnumerable<customerForList> displayCustomerList() //displays the list of customers
        {
            foreach (var cus in dl.PrintAllCustomer())
            {
                int notAcceptedPar = 0;
                int notDeliveredPar = 0;
                customer c = displayCustomer(cus.Id); //catch
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

