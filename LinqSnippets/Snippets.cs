using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LinqSnippets
{

    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Gol",
                "Fiat Palio",
                "Chevrolet Corsa",
                "Peugeot 206",
                "Peugeot 408",
                "Ford K",
                "Renault Clio"
            };

            // 1. SELECT *
            var carList = from car in cars select car;
            foreach (var car in carList) {
                Console.WriteLine(car);
            }

            // SELECT WHERE
            var peugeotList = from car in cars where car.Contains("Peugeot") select car;
            foreach (var peugeot in peugeotList)
            {
                Console.WriteLine(peugeot);
            }
        }

        // Numbers Examples
        static public void LinQNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Multiplicamos cada numero por 3,
            // obtenemos todos los numeros menos el 9,
            // ordenamos de manera ascendente
            var processedNumberList = numbers.Select(num => num * 3).Where(num => num !=9).OrderBy(num => num);
        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "VW Gol",
                "Fiat Palio",
                "Chevrolet Corsa",
                "Peugeot 206",
                "Peugeot 408",
                "Ford K",
                "Renault Clio"
            };

            // 1. Primero de los elementos
            var first = textList.First();

            // 2. Primer elemento que tenga la letra c
            var cText = textList.First(text => text.Contains("c"));

            // 3. Elementos que comiencen con la letra P
            var pText = textList.All(text => text.StartsWith("P"));

            // 4. El primer elemento que contenga Z y sino un valor por default
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("Z"));

            // 4. El ultimo elemento que contenga Z y sino un valor por default
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("Z"));

            // 5. Que contenga un unico valor
            var uniqueTexts = textList.Single();
            var uniqueOrDefault = textList.SingleOrDefault();

            
            // Obtener 4 y 8
            int[] evenNumbers = { 2, 4, 6, 8 };
            int[] otherNumbers = { 1, 2, 3, 4, 5, 6, 7, 8};

            var myEvenNumbers = evenNumbers.Except(otherNumbers);
        }

        static public void MultipleSelects()
        {
            // Select Many // divido por coma
            string[] myCars =
            {
                "VW, Gol",
                "Fiat, Palio",
                "Chevrolet, Corsa"
            };

            var myCarsSelection = myCars.SelectMany(car => car.Split(" , "));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id= 1,
                    Name = "Empresa 1",
                    Employees= new[]
                    {
                        new Employee
                        {
                            Id= 1,
                            Name = "Franco",
                            Email = "fdg2112@gmail",
                            Salary = 100000
                        },
                        new Employee
                        {
                            Id= 2,
                            Name = "Pablo",
                            Email = "p12@gmail",
                            Salary = 500000
                        },
                        new Employee
                        {
                            Id= 3,
                            Name = "Tato",
                            Email = "fdg@gmail",
                            Salary = 200000
                        }
                    }
                },
                new Enterprise()
                {
                    Id= 2,
                    Name = "Empresa 2",
                    Employees= new[]
                    {
                        new Employee
                        {
                            Id= 1,
                            Name = "Mar",
                            Email = "mar2@gmail",
                            Salary = 1300000
                        },
                        new Employee
                        {
                            Id= 2,
                            Name = "Agu",
                            Email = "agg@gmail",
                            Salary = 1500000
                        }
                    }
                }
            };

            // Obtener todos los empleados de todas las empresas
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Saber si cualquier lista está vacía
            bool hasEnterprises = enterprises.Any();

            // Saber si hay una lista dentro de la lista de empresas
            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // Comprobar si todas tienen empleados con salario mayor a 999999
            bool hasEmployeeWithSalaryMoreThan999999 = enterprises.Any(enterprise => enterprise.Employees.Any(employee => employee.Salary > 999999));
        }
        
        static public void LinqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "b", "c", "d" };

            // Inner Join
            var commonResult = from element in firstList 
                               join element2 in secondList
                               on element equals element2
                               select new { element, element2 };

            var commonResult2 = firstList.Join(
                secondList,
                element => element,
                element2 => element2,
                (element, element2) => new {element, element2}
                );

            // Outer Join - Left
            var leftOuterJoin = from element in firstList
                                join element2 in secondList
                                on element equals element2 // obtengo los elementos en comun de ambas listas
                                into temporalList //guardo los elementos en una lista temporal
                                from temporalElement in temporalList.DefaultIfEmpty() //tomo los elementos de la lista temporal
                                where element != temporalElement // comparo y resto los elementos de la lista izquierda (firstList)
                                select new { Element = element }; //muestro los resultados

            // simplificado
            var leftOuterJoin2 = from element in firstList
                                 from element2 in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, Element2 = element2 };

            // Outer Join - Right
            var rightOuterJoin = from element2 in secondList
                                join element in firstList
                                on element2 equals element 
                                into temporalList 
                                from temporalElement in temporalList.DefaultIfEmpty() 
                                where element2 != temporalElement 
                                select new { Element = element2 };

            // Union
            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            // Skip

            var skipTwoFirstValues = myList.Skip(2); // Devuelve {3,4,5,6,7,8,9,10}

            var skipTwoLastValues = myList.SkipLast(2); // Devuelve {1,2,3,4,5,6,7,8}

            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); // Devuelve {4,5,6,7,8,9,10}

            // Take

            var takeTwoFirstValues = myList.Take(2); // Devuelve {1,2}

            var takeTwoLastValues = myList.TakeLast(2); // Devuelve {9,10}

            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // { 1, 2, 3 }
        }
    }
}