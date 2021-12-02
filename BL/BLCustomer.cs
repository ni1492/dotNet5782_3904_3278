using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
namespace IBL
{
    public partial class BL : IBL
    {
        public void addCustomer(customer customer)
        {
            try
            {
                dl.AddCustomer(customer.id, customer.name, customer.phone, customer.location.Longitude, customer.location.Latitude);
            }
            catch
            {
                //exception - exists
            }

        }
        public void updateCustomer(int id, string name, string phone)
        {
            IDAL.DO.Customer tempDL = dl.PrintCustomer(id);
            dl.deleteCustomer(id);
            if (name != null)
                tempDL.Name = name;
            if (phone != null)
                tempDL.Phone = phone;
            dl.AddCustomer(tempDL.Id, tempDL.Name, tempDL.Phone, tempDL.Longitude, tempDL.Lattitude);
        }
        public customer displayCustomer(int id)
        {
            IDAL.DO.Customer cus = dl.PrintCustomer(id);
            List<parcelAtCustomer> fromCus = new();
            List<parcelAtCustomer> toCus = new();
            foreach (var p in displayParcelList())
            {
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
                            id = dl.PrintAllCustomer().ToList().Find(customer => customer.Name == p.sender).Id

                        }
                    });
                }
                if (p.sender == cus.Name)
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
                            id = dl.PrintAllCustomer().ToList().Find(customer => customer.Name == p.receiver).Id

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

        public IEnumerable<customerForList> displayCustomerList()
        {
            foreach (var cus in dl.PrintAllCustomer())
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

