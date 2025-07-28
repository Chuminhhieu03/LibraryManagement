namespace LibraryManagement.Domain.Enums
{
    public enum NotificationType
    {
        BookOverdue = 1,
        BookReturn = 2,
        BookReservationAvailable = 3,
        NewBookArrival = 4,
        LibraryEvent = 5,
        RoomAvailable = 6,
        AssetMalfunction = 7,
        MembershipExpiring = 8,
        FineNotification = 9,
        SystemMaintenance = 10,
        GeneralAnnouncement = 11,
        BookRenewalReminder = 12,
        RoomReservationReminder = 13,
        AssetMaintenanceScheduled = 14
    }

    public enum AssetType
    {
        Furniture = 1,
        Computer = 2,
        Equipment = 3,
        Shelf = 4,
        Desk = 5,
        Chair = 6,
        Printer = 7,
        Projector = 8,
        AirConditioner = 9,
        Light = 10
    }

    public enum AssetStatus
    {
        Available = 1,
        InUse = 2,
        UnderMaintenance = 3,
        Damaged = 4,
        Retired = 5,
        Reserved = 6
    }

    public enum RoomType
    {
        StudyRoom = 1,
        MeetingRoom = 2,
        ConferenceRoom = 3,
        GroupStudyRoom = 4,
        SilentStudyRoom = 5,
        ComputerLab = 6,
        ReadingRoom = 7,
        PrivateRoom = 8
    }

    public enum RoomStatus
    {
        Available = 1,
        Occupied = 2,
        Reserved = 3,
        UnderMaintenance = 4,
        Closed = 5
    }

    public enum ReservationStatus
    {
        Pending = 1,
        Confirmed = 2,
        InProgress = 3,
        Completed = 4,
        Cancelled = 5,
        NoShow = 6
    }

    public enum VisitPurpose
    {
        Reading = 1,
        StudyingAlone = 2,
        GroupStudy = 3,
        Research = 4,
        Meeting = 5,
        BookBorrowing = 6,
        BookReturning = 7,
        ComputerUse = 8,
        Event = 9,
        Other = 10
    }
}