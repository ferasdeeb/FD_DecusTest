using DecusTest_NewStructure.Enums;

namespace DecusTest_NewStructure.Rater.OptionalCovers
{
   public class OptionalCoverageA() : OptionalCoverBase, IOptionalCover
    {

        private CoverAOptionsEnum _defaultValue = CoverAOptionsEnum.Excluded;
        public CoverAOptionsEnum DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

    }
}
