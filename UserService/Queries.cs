namespace UserService
{
    public class Queries
    {
        public static string SelectAllUsers = @"
            SELECT *
            FROM Users
        ";

        public static string InsertUser = @"
            INSERT INTO USERS
            VALUES (@Id, @Name, @Status)
        ";

        public static string IsExists = @"
           SELECT TOP 1 *
           FROM Users
           WHERE Id = @Id
        ";
    }
}
