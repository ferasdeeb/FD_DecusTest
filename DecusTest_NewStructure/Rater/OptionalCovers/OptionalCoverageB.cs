using DecusTest_NewStructure.Enums;

namespace DecusTest_NewStructure.Rater.OptionalCovers
{
    public class OptionalCoverageB() : OptionalCoverBase, IOptionalCover
    {
        private CoverBOptionsEnum _defaultValue = CoverBOptionsEnum._10;
        public CoverBOptionsEnum DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public override bool CanAddToPremium() =>  base.IsAvailable && DefaultValue != CoverBOptionsEnum.Excluded;

    }
}
