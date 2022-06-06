using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace PolarCapeAssignment
{
    class Program
    {
        static void Main(string[] args)
        {

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                //CreateTables(connection);

                //var cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (1, 'Belinda Wynn', '123321456654', 'belinda@gmail.com')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (2, 'Jawad Beard', '1543543354', 'jawad@gmail.com')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (3, 'Shola Sherman', '143543354', 'shola@gmail.com')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (4, 'Fannie Hammond', '4398789954', 'fannie@gmail.com')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (5, 'Subhaan Simon', '31455436544', 'subhaan@gmail.com')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (6, 'Janae Davie', '9873543354', 'janae@gmail.com')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (7, 'Siraj Mckenna', '1977643123', 'siraj@gmail.com')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (8, 'Masuma Yu', '9876543214', 'masuma@gmail.com')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (9, 'Alanna Green', '125474523454', 'alanna@gmail.com')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (10, 'Tamar Carr', '15524635439', 'tamar@gmail.com')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (11, 'Kavan Kidd', '1399549994', 'kavan@gmail.com')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Employee(employeeNumber, fullName, mobilePhone, emailAddress) VALUES (12, 'Paula Prentice', '9511483354', 'paula@gmail.com')", connection);
                //cmd.ExecuteNonQuery();

                //var cmd = new SqlCommand("INSERT INTO Client(id, fullName, mobilePhone, emailAddress, company) VALUES (1, 'Caolan Zhang', '111333444555', 'caolan@gmail.com', 'Coca Cola')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Client(id, fullName, mobilePhone, emailAddress, company) VALUES (2, 'Brandon Russell', '666444555999', 'brandon@gmail.com', 'Pepsi')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Client(id, fullName, mobilePhone, emailAddress, company) VALUES (3, 'Maria Fisher', '444333777555', 'maria@gmail.com', 'Adidas')", connection);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("INSERT INTO Client(id, fullName, mobilePhone, emailAddress, company) VALUES (4, 'Rea Salinas', '888444333222', 'rea@gmail.com', 'Nike')", connection);
                //cmd.ExecuteNonQuery();

                //getProject(connection);

                Console.WriteLine("Enter '1' to see the available actions.");
                Console.WriteLine("Enter '2' to to get a list of all clients.");
                Console.WriteLine("Enter '3' to get a list of all employees.");
                Console.WriteLine("Enter '4' to create a project.");
                Console.WriteLine("Enter '5' to get a list of projects.");
                Console.WriteLine("Enter '6' to exit the application.");

                while (true)
                {
                    var input = Console.ReadLine();
                    if (input.Equals("1", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Enter '1' to see the available actions.");
                        Console.WriteLine("Enter '2' to to get a list of all clients.");
                        Console.WriteLine("Enter '3' to get a list of all employees.");
                        Console.WriteLine("Enter '4' to create a project.");
                        Console.WriteLine("Enter '5' to get a list of projects.");
                        Console.WriteLine("Enter '6' to exit the application.");
                    }

                    if (input.Equals("2", StringComparison.OrdinalIgnoreCase))
                    {
                        getClients(connection);
                    }

                    if (input.Equals("3", StringComparison.OrdinalIgnoreCase))
                    {
                        getEmployee(connection);
                    }

                    if (input.Equals("4", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Enter project name: ");
                        var title = Console.ReadLine();
                        while (title == "")
                        {
                            Console.WriteLine("Please enter title of the project");
                            title = Console.ReadLine();
                        }

                        Console.WriteLine("Title of your new project is " + title + "\n");
                        Console.WriteLine("Please enter the start date of the project with format YYYY-MM-DD");
                        var startDate = Console.ReadLine();
                        while (startDate == "")
                        {
                            Console.WriteLine("Please enter the start date of the project with format YYYY-MM-DD");
                            startDate = Console.ReadLine();
                        }
                        Console.WriteLine("Start date of your new project is " + startDate + "\n");
                        Console.WriteLine("Please enter the end date of the project with format YYYY-MM-DD \nThis is optional, you can leave it blank by pressing enter");
                        var endDate = Console.ReadLine();

                        Console.WriteLine("Choose company id for which this project is meant to");
                        List<Client> clients = GetClientsToList(connection);

                        foreach (Client client in clients)
                        {
                            Console.WriteLine(client.id + " - " + client.fullName);
                        }

                        var companyFk = Console.ReadLine();

                        var query = new SqlCommand("SELECT COUNT(*) FROM Project", connection);
                        var reader = query.ExecuteReader();

                        int numberOfProjects = 0;
                        while (reader.Read())
                        {
                            numberOfProjects = Convert.ToInt32(String.Format("{0}", reader[0]));
                        }

                        reader.Close();

                        int projectID = numberOfProjects + 1;

                        var cmd = new SqlCommand("INSERT INTO Project(Id, name, startDate, endDate, clientFk) VALUES (" + projectID + ",'" + title + "', " + "'" + startDate + "'" + ", " + "'" + endDate + "'" + ", " + companyFk + ")", connection);
                        cmd.ExecuteNonQuery();

                        Console.WriteLine("Choose how much employees are you going to assign to this project.");
                        var numberOfEmployeesChosen = Console.ReadLine();

                        int numOfEmployeesChosen = Convert.ToInt32(numberOfEmployeesChosen);
                        List<Employee> employees = GetEmployeesList(connection);

                        while (numOfEmployeesChosen > 0)
                        {
                            foreach (Employee employee in employees)
                            {
                                Console.WriteLine(employee.EmployeeNumber + " - " + employee.fullName);
                            }
                            var employeeNumber = Console.ReadLine();

                            query = new SqlCommand("SELECT COUNT(*) FROM ProjectEmployee", connection);
                            reader = query.ExecuteReader();

                            int numberOfProjectEmployees = 0;
                            while (reader.Read())
                            {
                                numberOfProjectEmployees = Convert.ToInt32(String.Format("{0}", reader[0]));
                            }

                            reader.Close();


                            int projectEmployeeId = numberOfProjectEmployees + 1;

                            cmd = new SqlCommand("INSERT INTO ProjectEmployee(id, projectId , employeeId) VALUES (" + projectEmployeeId + ", " + projectID + ", " + employeeNumber + ")", connection);
                            cmd.ExecuteNonQuery();

                            Console.WriteLine("SUCCESS");
                            int employeeUsed = employees.IndexOf(employees.FirstOrDefault(e => e.EmployeeNumber == Convert.ToInt32(employeeNumber)));

                            employees.RemoveAt(employeeUsed);

                            numOfEmployeesChosen--;
                        }

                        Console.WriteLine("SUCCESS");



                    }

                    if (input.Equals("5", StringComparison.OrdinalIgnoreCase))
                    {
                        getProject(connection);
                    }

                    if (input.Equals("6", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }










                //DropTables(connection);
                // Do work here; connection closed on following line.
            }
        }

        public static void getClients(SqlConnection connection)
        {
            var cmd = new SqlCommand("SELECT * FROM Client", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0}, {1}, {2}, {3}, {4}", reader[0], reader[1], reader[2], reader[3], reader[4]));
            }
            reader.Close();
        }

        public static void getEmployee(SqlConnection connection)
        {
            var cmd = new SqlCommand("SELECT * FROM Employee", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0}, {1}, {2}, {3}", reader[0], reader[1], reader[2], reader[3]));
            }
            reader.Close();
        }

        public static List<Employee> GetEmployeesList(SqlConnection connection)
        {
            List<Employee> employees = new List<Employee>();

            var cmd = new SqlCommand("SELECT * FROM Employee", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var employeeNumber = String.Format("{0}", reader[0]);
                var fullName = String.Format("{0}", reader[1]);
                var mobilePhone = String.Format("{0}", reader[2]);
                var emailAddress = String.Format("{0}", reader[3]);

                employees.Add(new Employee(Convert.ToInt32(employeeNumber), fullName, mobilePhone, emailAddress));
            }
            reader.Close();

            return employees;
        }

        public static void getProject(SqlConnection connection)
        {
            var cmd = new SqlCommand("SELECT * FROM Project", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0}, {1}, {2}, {3}", reader[0], reader[1], reader[2], reader[3]));
            }
            reader.Close();
        }

        public static List<Client> GetClientsToList(SqlConnection connection)
        {
            List<Client> clients = new List<Client>();
            var cmd = new SqlCommand("SELECT * FROM Client", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var id = String.Format("{0}", reader[0]);
                var fullName = String.Format("{0}", reader[1]);
                var mobilePhone = String.Format("{0}", reader[2]);
                var emailAddress = String.Format("{0}", reader[3]);
                var company = String.Format("{0}", reader[4]);

                clients.Add(new Client(Convert.ToInt32(id), fullName, mobilePhone, emailAddress, company));
            }
            reader.Close();

            return clients;
        }

        static private void CreateTables(SqlConnection connection)
        {
            string createTables = "CREATE TABLE Client(Id INTEGER PRIMARY KEY, FullName VARCHAR(50), MobilePhone VARCHAR(15), EmailAddress VARCHAR(50), Company VARCHAR(50))" +
                        "CREATE TABLE Employee(EmployeeNumber INTEGER PRIMARY KEY, FullName VARCHAR(50), MobilePhone VARCHAR(15), EmailAddress VARCHAR(50))" +
                        "CREATE TABLE Project(Id INT PRIMARY KEY, Name VARCHAR(50), StartDate DATE, EndDate DATE, ClientFK INT)" +
                        "CREATE TABLE ProjectEmployee(Id INT PRIMARY KEY, ProjectId INT, EmployeeId INT)";

            var cmd = new SqlCommand(createTables, connection);

            cmd.ExecuteNonQuery();
        }

        static private void DropTables(SqlConnection connection)
        {
            string dropTables = "DROP TABLE Client;DROP TABLE Employee;DROP TABLE Project";
            var cmd = new SqlCommand(dropTables, connection);
            cmd.ExecuteNonQuery();
        }

        static private string GetConnectionString()
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file, using the
            // System.Configuration.ConfigurationSettings.AppSettings property
            return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PolarCape;Integrated Security=True;";
        }
    }

    public class Project
    {
        public String name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int clientFk { get; set; }

        public Project(string name, DateTime startDate, DateTime endDate, int clientFk)
        {
            this.name = name;
            this.startDate = startDate;
            this.endDate = endDate;
            this.clientFk = clientFk;
        }
    }

    public class ProjectEmployee
    {
        public int Int { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }

        public ProjectEmployee(int @int, int projectId, int employeeId)
        {
            Int = @int;
            ProjectId = projectId;
            EmployeeId = employeeId;
        }
    }

    public class Employee
    {
        public int EmployeeNumber { get; set; }
        public string fullName { get; set; }
        public string mobilePhone { get; set; }
        public string emailAddress { get; set; }

        public Employee(int employeeNumber, string fullName, string mobilePhone, string emailAddress)
        {
            this.EmployeeNumber = employeeNumber;
            this.fullName = fullName;
            this.mobilePhone = mobilePhone;
            this.emailAddress = emailAddress;
        }
    }

    public class Client
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string mobilePhone { get; set; }
        public string emailAddress { get; set; }
        public string company { get; set; }

        public Client(int id, string fullName, string mobilePhone, string emailAddress, string company)
        {
            this.id = id;
            this.fullName = fullName;
            this.mobilePhone = mobilePhone;
            this.emailAddress = emailAddress;
            this.company = company;
        }
    }


}
