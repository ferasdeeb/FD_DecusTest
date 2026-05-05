using DecusTest_NewStructure.Enums;

namespace DecusTest_NewStructure.Rater.OptionalCovers
{
    public class OptionalCoverageC() : OptionalCoverBase, IOptionalCover
    {
        private CoverCOptionsEnum _defaultValue = CoverCOptionsEnum._10000;
        public CoverCOptionsEnum DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

    }
}
