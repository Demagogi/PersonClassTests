using System;
using System.Collections.Generic;
using System.IO;


/* გაქვს კლასი Person რაღაც ველებით სახელი გვარი ასაკი.. ასევე აქვს ველი მშობელი Person ტიპის და აქვს შვილები List<Person> ტიპის. 
შენი მიზანი არის შემდეგი: 
ქმნი პერსონის ობიექტს და ამ ობიექტს რამენაირად(სრული თავისუფლება გაქ) წერ ფაილში. 
შემდეგ კიდე უნდა შეგეძლოს ფაილიდან მთლიანი ობიექტი აღადგინო კოდში 
ანუ ფაილიდან წაიკითხო მონაცემები და იმის მიხედვით კოდში ისევ შექმნა პერსონის ობიექტი
*/

public class Person
{
    // Attributes
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Age { get; set; }
    public List<Person>? Children { get; set; }

    // Constructor
    public Person(string? firstName = null, string? lastName = null, int? age = null, List<Person>? children = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Children = children;
    }

    /// <summary>
    /// Converts Person Object To serialized string
    /// </summary>
    /// <param name="person"></param>
    /// <returns>String</returns>
    public string Serialization()
    {
        string childrenToString = "";
        if (Children != null && Children.Count > 0)
        {
            childrenToString += "Children:[";
            for (int i = 0; i < Children.Count; i++)
            {
                childrenToString += Children[i].Serialization();
                if (i < Children.Count - 1)
                {
                    childrenToString += ",";
                }
            }
            childrenToString += "]";
        }
        return $"Name:{FirstName}, LastName:{LastName}, Age:{Age}, {childrenToString}";
    }

    /// <summary>
    /// Converts serialized string to a Person Object
    /// </summary>
    /// <param name="person"></param>
    /// <returns>New Person Object</returns>
    public static Person Deserialization(string person)
    {
        Person recreatedPerson = new Person();
        int startIndex, endIndex;

        //Parse FirstName property
        startIndex = person.IndexOf("Name:") + 5; // 5 aris "Name:" - amis sigrdze
        endIndex = person.IndexOf(", LastName:", startIndex); //11
        recreatedPerson.FirstName= person.Substring(startIndex, endIndex - startIndex);

        //Parse LastName property
        startIndex = endIndex + 11; // 11 aris , LastName:" amis sigrdze
        endIndex = person.IndexOf(", Age:", startIndex);
        recreatedPerson.LastName = (person.Substring(startIndex, endIndex - startIndex));
  
        //Parse Age property
        startIndex = endIndex + 6; // 6 aris ", Age:" - amis sigrdze
        endIndex = person.Contains(", Children") ? person.IndexOf(", Children:", startIndex) : person.Length;
        recreatedPerson.Age = int.Parse(person.Substring(startIndex, endIndex - startIndex).Trim(',', ' '));

        //Parse Children property
        startIndex = endIndex + 12; // 12 aris ", Children:" - amis sigrdze
        endIndex = person.Length > 0 ? person.Length - 1 : 0;

        if(startIndex > 11 && endIndex > startIndex)
        {
            string childrensString = person.Substring(startIndex, endIndex - startIndex);
            recreatedPerson.Children = new List<Person>();

            while (childrensString.Length > 1)
            {
                int endObjectIndex = childrensString.Length > 0 ? childrensString.Length - 1: 0 ;
                string childString = childrensString.Substring(0, endObjectIndex);
                Person child = Person.Deserialization(childString);
                recreatedPerson.Children.Add(child);
                childrensString = childrensString.Substring(endObjectIndex);

                if (childrensString.StartsWith(","))
                {
                    childrensString = childrensString.Substring(1);
                }
            }
        }
        return recreatedPerson;
    }
}

public static class FileManager
{
    static public void Sw (string path, string info)
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.WriteLine(info);
        }
    }

    static public string Sr (string path)
    {
        string data = "";
        using (StreamReader sr = new StreamReader(path))
        {
            data = sr.ReadLine();
        }
        return data;
    }
}