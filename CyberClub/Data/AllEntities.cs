namespace CyberClub.Data
{
    public partial class Dev
    {
        public override string ToString() => DevID.ToString();
    }
    public partial class Game
    {
        public override string ToString() => GameID.ToString();
    }
    public partial class Genre
    {
        public override string ToString() => GenreName;
    }
    public partial class Pic
    {
        public override string ToString() => PicID.ToString();
    }
    public partial class User
    {
        public override string ToString() => UserID.ToString();
    }
}