namespace CyberClub.Data
{
    public partial class Developer
    {
        public override string ToString() => DeveloperID.ToString();
    }
    public partial class Game
    {
        public override string ToString() => GameID.ToString();
    }
    public partial class Genre
    {
        public override string ToString() => GenreName;
    }
    public partial class Image
    {
        public override string ToString() => ImageID.ToString();
    }
    public partial class User
    {
        public override string ToString() => UserID.ToString();
    }

    public enum UserLevel
    {
        Admin = 0,
        Player = 1,
        Banned = 2
    }
}