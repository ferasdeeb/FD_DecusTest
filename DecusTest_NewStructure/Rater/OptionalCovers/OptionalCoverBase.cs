namespace DecusTest_NewStructure.Rater.OptionalCovers
{
    public abstract class OptionalCoverBase : IOptionalCover
    {

        private bool _isAvailable = true;
        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _niceName = "";

        public string NiceName
        {
            get { return _niceName; }
            set { _niceName = value; }
        }


    }

}
