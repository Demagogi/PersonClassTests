using Xunit;

namespace TestRecreatedObject
{
    public class PersonObject
    {
 
        [Fact]
        public void TestPersonIrakli()
        {
            //Arrange
            string path = ""; // add your file location here

            Person testPerson1 = new Person("TestName", "TestLastName", 50, new List<Person>());
            Person testPerson2 = new Person("TestChildsName", "TestLastname", 24, new List<Person>());

            testPerson1.Children.Add(testPerson2);

            string expectedName = "TestName";
            string expectedLastName = "TestLastName";
            int expectedAge = 50;
            int expectedChildrenCount = 1;

            //Act
            string personToString = testPerson1.Serialization();
            FileManager.Sw(path, personToString);

            string personString = FileManager.Sr(path);
            var recreatedPerson = Person.Deserialization(personString);

            //Assert
            Assert.Equal(expectedName, recreatedPerson.FirstName);
            Assert.Equal(expectedLastName, recreatedPerson.LastName);
            Assert.Equal(expectedAge, recreatedPerson.Age);
            Assert.Equal(expectedChildrenCount, recreatedPerson.Children.Count);
        }

        [Fact]
        public void TestPersonGio()
        {
            //Arrange
            string path = ""; // add your file location here

            Person testPerson1 = new Person("TestName", "TestLastName", 50, new List<Person>());
            Person testPerson2 = new Person("TestChildsName", "TestLastName", 11);

            testPerson1.Children.Add(testPerson2);

            string expectedName = "TestName";
            string expectedLastName = "TestLastName";
            int expectedAge = 50;
            int expectedChildrenCount = 1;

            //Act
            string personToString = testPerson1.Serialization();
            FileManager.Sw(path, personToString);

            string personString = FileManager.Sr(path);
            var recreatedPerson = Person.Deserialization(personString);

            //Assert
            Assert.Equal(expectedName, recreatedPerson.FirstName);
            Assert.Equal(expectedLastName, recreatedPerson.LastName);
            Assert.Equal(expectedAge, recreatedPerson.Age);
            Assert.Equal(expectedChildrenCount, recreatedPerson.Children.Count);
        }

        [Fact]
        public void TestPersonDato()
        {
            //Arrange
            string path = ""; // add your file location here

            Person testPerson = new Person("TestName", "TestLastName", 11);

            string expectedName = "TestName";
            string expectedLastName = "TestLastName";
            int expectedAge = 11;


            //Act
            string personToString = testPerson.Serialization();
            FileManager.Sw(path, personToString);

            string personString = FileManager.Sr(path);
            var recreatedPerson = Person.Deserialization(personString);

            //Assert
            Assert.Equal(expectedName, recreatedPerson.FirstName);
            Assert.Equal(expectedLastName, recreatedPerson.LastName);
            Assert.Equal(expectedAge, recreatedPerson.Age);
        }

        [Fact]
        public void TestPersonsTogather()
        {
            //Arrange
            string path = ""; // add your file location here

            Person testPerson1 = new Person("TestName", "TestLastName", 50, new List<Person>());
            Person testPerson2 = new Person("TestName", "TestLastName", 24, new List<Person>());
            Person testPerson3 = new Person("TestName", "TestLastName", 11);

            testPerson1.Children.Add(testPerson2);
            testPerson1.Children.Add(testPerson3);

            string expectedName = "TestName";
            string expectedLastName = "TestLastName";
            int expectedAge = 50;
            int expectedChildrenCount = 1;

            string expectedChildName = "TestName";
            string expectedChildLastName = "TestLastName";
            int expectedChildAge = 24;
            int expectedChildChildrenCount = 1;

            string expectedChildsChildName = "TestName";
            string expectedChildsChildLastName = "TestLastName";
            int expectedChildsChildAge = 11;

            //Act
            string personToString = testPerson1.Serialization();
            FileManager.Sw(path, personToString);

            string personString = FileManager.Sr(path);
            var recreatedPerson = Person.Deserialization(personString);

            //Assert
            Assert.Equal(expectedName, recreatedPerson.FirstName);
            Assert.Equal(expectedLastName, recreatedPerson.LastName);
            Assert.Equal(expectedAge, recreatedPerson.Age);
            Assert.Equal(expectedChildrenCount, recreatedPerson.Children.Count);

            Assert.Equal(expectedChildName, recreatedPerson.Children[0].FirstName);
            Assert.Equal(expectedChildLastName, recreatedPerson.Children[0].LastName);
            Assert.Equal(expectedChildAge, recreatedPerson.Children[0].Age);
            Assert.Equal(expectedChildChildrenCount, recreatedPerson.Children[0].Children.Count);

            Assert.Equal(expectedChildsChildName, recreatedPerson.Children[0].Children[0].FirstName);
            Assert.Equal(expectedChildsChildLastName, recreatedPerson.Children[0].Children[0].LastName);
            Assert.Equal(expectedChildsChildAge, recreatedPerson.Children[0].Children[0].Age);
        }
    }
}