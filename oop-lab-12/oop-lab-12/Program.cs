using System;
using System.Reflection;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab_12
{
    class Program
    {

        static void Main(string[] args)
        {
            //string str = typeof(System.DateTime).ToString();
            //для встроенного класса
            Reflector.WriteAllMembersInFile("System.DateTime", "Task_1_for_DateTime.txt");

            Reflector.GetAllPublicMethods("System.DateTime");
            Reflector.GetAllFieldsAndProperties("System.DateTime");
            Reflector.GetAllInterfaces("System.DateTime");

            Reflector.PrintMenthodsThatContainsParameter("System.DateTime", "System.DateTime");


            //для пользовательского типа
            Reflector.WriteAllMembersInFile("oop_lab_12.Goods", "Task_1_for_Goods.txt");

            Reflector.GetAllPublicMethods("oop_lab_12.Goods");
            Reflector.GetAllFieldsAndProperties("oop_lab_12.Goods");
            Reflector.GetAllInterfaces("oop_lab_12.Goods");

            Reflector.PrintMenthodsThatContainsParameter("oop_lab_12.Goods", "System.Void");


            string AssemblyLocation = "D:\\дз\\ДЗ        2 курс\\ООП\\7\\oop-lab-7\\oop-lab-7\\bin\\Debug\\oop-lab-7.exe";
            Reflector.InvokeMethod("oop_lab_12.Table", "MethodForLab12", "6.txt",AssemblyLocation);
           // Reflector.InvokeMethod("oop_lab_12.Technique", "MethodForLab12", "6.txt", AssemblyLocation);
           // Reflector.InvokeMethod("oop_lab_12.Screan", "MethodForLab12", "6.txt", AssemblyLocation);



        }
    }
}