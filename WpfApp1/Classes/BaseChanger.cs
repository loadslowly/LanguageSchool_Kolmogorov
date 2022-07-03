using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataBase;

namespace WpfApp1.Classes
{
    class BaseChanger
    {
        public static void DeleteService(int serviceId)
        {
            var data = LanguageSchoolEntities.GetContext();

            var selectService = from service in data.Service
                                where service.ID == serviceId
                                select service;

            data.Service.Remove(selectService.FirstOrDefault());
            data.SaveChanges();
        }
    }
}
