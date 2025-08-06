namespace LibraryManagement.Domain.Enums
{
    /// <summary>
    /// Enumerates the types of notifications within the Library Management System.
    /// </summary>
    /// <remarks>
    /// The NotificationType enum identifies various types of notifications that can
    /// be sent to library members or administrators.
    /// </remarks>
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

    /// <summary>
    /// Enumerates the various types of assets managed within the Library Management System.
    /// </summary>
    /// <remarks>
    /// The AssetType enum classifies the physical and digital assets available in the library,
    /// such as furniture, electronic devices, and equipment.
    /// </remarks>
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

    /// <summary>
    /// Represents the possible statuses of an asset within the library system.
    /// </summary>
    /// <remarks>
    /// The AssetStatus enum specifies the current state of an asset, offering a way to track
    /// its availability, usage condition, and operational state within the library system.
    /// </remarks>
    public enum AssetStatus
    {
        Available = 1,
        InUse = 2,
        UnderMaintenance = 3,
        Damaged = 4,
        Retired = 5,
        Reserved = 6
    }

    /// <summary>
    /// Represents the types of rooms available in the library system.
    /// </summary>
    /// <remarks>
    /// The RoomType enum is used to categorize different types of rooms
    /// that can be managed and utilized within the library infrastructure.
    /// </remarks>
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

    /// <summary>
    /// Defines the possible statuses for rooms within the Library Management System.
    /// </summary>
    /// <remarks>
    /// The RoomStatus enum is used to represent the current status of a room,
    /// providing information regarding its availability, usage, and condition.
    /// </remarks>
    public enum RoomStatus
    {
        Available = 1,
        Occupied = 2,
        Reserved = 3,
        UnderMaintenance = 4,
        Closed = 5
    }

    /// <summary>
    /// Represents the various statuses a reservation can have within the Library Management System.
    /// </summary>
    /// <remarks>
    /// The ReservationStatus enum identifies the lifecycle states that a reservation, such as
    /// a room or book reservation, may go through during the process of booking and usage.
    /// </remarks>
    public enum ReservationStatus
    {
        Pending = 1,
        Confirmed = 2,
        InProgress = 3,
        Completed = 4,
        Cancelled = 5,
        NoShow = 6
    }

    /// <summary>
    /// Enumerates the purposes for visiting the library within the Library Management System.
    /// </summary>
    /// <remarks>
    /// The VisitPurpose enum is used to categorize and identify the primary reason for a user's visit
    /// to the library facilities. This information can help in better resource allocation and
    /// understanding visitor trends.
    /// </remarks>
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