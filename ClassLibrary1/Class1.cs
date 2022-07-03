using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassLibrary1
{
    public class Class1
    {
        public static bool CheckAddEditService(int Title, int Cost, int DurationInSeconds, double Discount)
        {
            StringBuilder errors = new StringBuilder();

            if (Title == 0)
                errors.AppendLine("Нужно заполнить навзание услуги!");
            if (Cost == 0)
                errors.AppendLine("Нужно заполнить стоимость услуги!");
            if (DurationInSeconds == 0)
                errors.AppendLine("Нужно заполнить длительность услуги!");
            if (Discount < 0)
                errors.AppendLine("Скидка не может быть отрицательной!");
            if (Discount > 1)
                errors.AppendLine("Скидка не может быть больше 100%!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return false;
            }

            return true;
        }
    }
}
