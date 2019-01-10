using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab_12
{
    static class Reflector
    {

        //выводит всё содержимое класса в текстовый файл
        public static void WriteAllMembersInFile(string nameOfClass,string nameOfFile)
        {
            Type type = Type.GetType(nameOfClass, false);
            
            using (StreamWriter writer = new StreamWriter(nameOfFile))
            {
                foreach (MemberInfo mi in type.GetMembers())
                {
                    //Console.WriteLine(mi.DeclaringType + " " + mi.MemberType + " " + mi.Name);
                    //writer.WriteLine(mi);
                    writer.WriteLine(mi.DeclaringType + " " + mi.MemberType + " " + mi.Name);
                }
            }

        }

        public static void GetAllPublicMethods(string nameOfClass)
        {
            Type type = Type.GetType(nameOfClass, false, true);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nМетоды:\n");
            Console.ResetColor();
            foreach (MethodInfo method in type.GetMethods())
            {
                string modificator = "";

                if (method.IsPublic)
                {
                    modificator += "public ";
                    if (method.IsStatic)
                        modificator += "static ";
                    if (method.IsVirtual)
                        modificator += "virtual ";
                    Console.Write(modificator + method.ReturnType.Name + " " + method.Name + " (");
                    //получаем все параметры
                    ParameterInfo[] parameters = method.GetParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        Console.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                        if (i + 1 < parameters.Length) Console.Write(", ");
                    }
                    Console.WriteLine(")");
                }
            }                       
        }

        public static void GetAllFieldsAndProperties(string nameOfClass)
        {
            Type type = Type.GetType(nameOfClass, false, true);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nПоля:\n");
            Console.ResetColor();

            foreach(FieldInfo field in type.GetRuntimeFields())
            {
                Console.WriteLine(field.FieldType+"  "+field.Name);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nСвойства:\n");
            Console.ResetColor();

            foreach (PropertyInfo property in type.GetRuntimeProperties())
            {
                Console.WriteLine(property.PropertyType + " " + property.Name);
            }
        }

        public static void GetAllInterfaces(string nameOfClass)
        {
            Type type = Type.GetType(nameOfClass, false, true);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nИнтерфейсы:\n");
            Console.ResetColor();

            foreach (Type i in type.GetInterfaces())
            {
                Console.WriteLine(i.Name);
            }
        }

        public static void PrintMenthodsThatContainsParameter(string TypeOfFilde,string TypeOfParameter)
        {
            int counter = 0;
            Type type = Type.GetType(TypeOfFilde, false, true);
            Type typeOfParameter = Type.GetType(TypeOfParameter, false, true);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nВсе методы содержащие параметры типа: "+typeOfParameter+"\n");
            Console.ResetColor();

            foreach(MethodInfo method in type.GetMethods())
            {
                foreach(ParameterInfo parameter in method.GetParameters())
                {
                    if(parameter.ParameterType==typeOfParameter)
                    {
                        Console.Write(method.ReturnType.Name+" "+ method.Name+" (");
                        counter++;
                        ParameterInfo[] p = method.GetParameters();
                        for (int i=0;i<p.Length;i++)
                        {
                            Console.Write(p[i].ParameterType + " " + p[i].Name );
                            if (p.Length - i != 1) Console.Write(", ");
                        }
                        Console.WriteLine(");");
                        
                        break;
                    }
                }
            }
            if(counter==0)
            {
                Console.WriteLine("Тип не содержит методов с такими параметрами.");
            }
        }


        public static void InvokeMethod(string nameOfClass,string nameOfMethod,string nameOfFile,string AssemblyLocation)
        {
            object[] parameters = null;
            using (StreamReader reader = new StreamReader(nameOfFile))
            {
                parameters= reader.ReadToEnd().Split(' ');
            }

           // Type typeOfObject = Type.GetType(nameOfClass, true, true);

            Assembly asm = Assembly.LoadFrom(AssemblyLocation);

            Type type = asm.GetType(nameOfClass, true, true);

            object obj = Activator.CreateInstance(type);



            MethodInfo method = type.GetMethod(nameOfMethod);

            method.Invoke(obj, parameters);

        }
    }
}
