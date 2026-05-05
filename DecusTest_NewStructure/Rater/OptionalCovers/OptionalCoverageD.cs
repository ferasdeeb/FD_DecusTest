using DecusTest_NewStructure.Enums;

namespace DecusTest_NewStructure.Rater.OptionalCovers
{
    public class OptionalCoverageD : OptionalCoverBase, IOptionalCover
    {
        private CoverDOptionsEnum _defaultValue = CoverDOptionsEnum.NA;
        public CoverDOptionsEnum DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

    }
}
