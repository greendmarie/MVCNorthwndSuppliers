using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer.Models;
using MVCNorthwndCRUD.Models;

namespace MVCNorthwndCRUD.Mapping
{
    public static class SupplierMapper
    {
        public static SupplierPO MapDoToPO(SupplierDO from)
        {
            SupplierPO to = new SupplierPO();
            to.SupplierId = from.SupplierId;
            to.ContactName = from.ContactName;
            to.ContactTitle = from.ContactTitle;
            to.PostalCode = from.PostalCode;
            to.Country = from.Country;
            to.PhoneNumber = from.PhoneNumber;

            return to;
        }

        public static List<SupplierPO> MapDoToPO(List<SupplierDO>from)
        {
            List<SupplierPO> to = new List<SupplierPO>();

            if (from != null)
            {
                foreach(SupplierDO item in from)
                {
                    SupplierPO mappedItem = MapDoToPO(item);
                    to.Add(mappedItem);
                }
            }
            return to;
        }
    }
}