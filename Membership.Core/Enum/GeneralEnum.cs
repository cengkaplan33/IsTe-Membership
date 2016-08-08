namespace Membership.Core.Enum
{
    public static class GeneralEnum
    {
        public enum Status
        {
            Passive = 0,
            Active = 1,
        }

        public enum IsDeleted : byte
        {
            No = 0,
            Yes = 1,
        }
    }
}