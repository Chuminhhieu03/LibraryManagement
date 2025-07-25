# Library Management System - Clean Architecture Implementation

## C?u trúc d? án ?ã tri?n khai

```
LibraryManagement.sln
??? LibraryManagement/                    # Presentation Layer (Web API)
??? LibraryManagement.Application/        # Application Layer
??? LibraryManagement.Domain/            # Domain Layer
??? LibraryManagement.Infrastructure/    # Infrastructure Layer
??? LibraryManagement.Shared/           # Shared Layer
```

## ?? Các ch?c n?ng ?ã implement

### 1. **Check-in/Check-out Management**
- **Entity**: `MemberVisit`
- **Features**:
  - Qu?n lý gi? vào/ra c?a thành viên
  - Theo dõi m?c ?ích visit (??c sách, h?c nhóm, nghiên c?u...)
  - Tính toán th?i gian l?u trú
  - Theo dõi khu v?c truy c?p

### 2. **Library Asset Management**
- **Entities**: `LibraryAsset`, `Shelf`, `AssetMaintenanceLog`
- **Features**:
  - Qu?n lý bàn gh?, máy tính, thi?t b?
  - Theo dõi tr?ng thái và v? trí v?t d?ng
  - L?p l?ch b?o trì ??nh k?
  - Qu?n lý giá k? và v? trí l?u tr?

### 3. **Comprehensive Notification System**
- **Entity**: `Notification`
- **Notification Types**:
  - ?? Book overdue notifications
  - ?? New book arrival alerts
  - ??? Library event announcements
  - ?? Room availability notifications
  - ?? Asset malfunction alerts
  - ?? Membership expiring warnings
  - ?? Fine notifications
  - ?? Book renewal reminders
  - ?? Room reservation reminders
  - ??? Scheduled maintenance alerts

### 4. **Private Room Reservation System**
- **Entities**: `Room`, `RoomReservation`
- **Room Types**: Study rooms, meeting rooms, conference rooms, private rooms
- **Features**:
  - Room availability checking
  - Reservation approval workflow
  - Check-in/check-out for reservations
  - Cost calculation and payment tracking
  - Capacity and equipment management

## ??? Clean Architecture Implementation

### **Domain Layer** (`LibraryManagement.Domain`)
```
??? Common/
?   ??? BaseEntity.cs                    # Base classes cho entities
??? Entities/
?   ??? Member.cs                        # Member entity (updated)
?   ??? MemberVisit.cs                   # Check-in/out entity
?   ??? LibraryAsset.cs                  # Asset management entities
?   ??? Notification.cs                  # Notification system
?   ??? Room.cs                          # Room management entities
?   ??? ExistingEntities.cs              # Placeholder cho entities c?
??? Enums/
?   ??? CommonEnums.cs                   # T?t c? enums c?n thi?t
??? Interfaces/
    ??? IRepository.cs                   # Generic repository pattern
```

### **Application Layer** (`LibraryManagement.Application`)
```
??? DTOs/
?   ??? NewEntitiesDto.cs               # Data Transfer Objects
??? Interfaces/
?   ??? IServices.cs                    # Service interfaces
??? Services/                           # Business logic services (ch?a implement)
??? Mappings/                           # AutoMapper profiles (ch?a implement)
```

### **Infrastructure Layer** (`LibraryManagement.Infrastructure`)
```
??? Data/                               # DbContext (ch?a migrate)
??? Repositories/
?   ??? GenericRepository.cs            # Generic repository implementation
??? Services/                           # External services (ch?a implement)
```

## ?? Generic Repository Pattern

### **Features ?ã implement:**
- **Generic CRUD operations** cho t?t c? entities
- **Include support** cho eager loading
- **Pagination** v?i filtering và sorting
- **Bulk operations** cho hi?u su?t t?t h?n
- **Specific repository interfaces** cho complex queries
- **Unit of Work pattern** cho transaction management

### **Repository Usage Example:**
```csharp
// Generic usage
var members = await _unitOfWork.Repository<Member>().GetAllAsync();

// Specific repository usage
var overdueMembers = await _unitOfWork.Members.GetMembersWithOverdueBooksAsync();

// With includes
var memberWithVisits = await _unitOfWork.Repository<Member>()
    .GetByIdAsync(1, m => m.Visits, m => m.Notifications);
```

## ?? Các tr??ng thông báo ???c ?? xu?t

### **Notification Cases ?ã thi?t k?:**
1. **Book Management**
   - BookOverdue: "Sách quá h?n tr?"
   - BookReturn: "Nh?c nh? tr? sách"
   - BookReservationAvailable: "Sách ??t tr??c ?ã có s?n"
   - NewBookArrival: "Sách m?i v? th? vi?n"
   - BookRenewalReminder: "Nh?c nh? gia h?n sách"

2. **Library Operations**
   - LibraryEvent: "S? ki?n th? vi?n"
   - SystemMaintenance: "B?o trì h? th?ng"
   - GeneralAnnouncement: "Thông báo chung"

3. **Room & Asset Management**
   - RoomAvailable: "Phòng tr?ng"
   - RoomReservationReminder: "Nh?c nh? ??t phòng"
   - AssetMalfunction: "Thi?t b? h?ng"
   - AssetMaintenanceScheduled: "L?ch b?o trì thi?t b?"

4. **Member Services**
   - MembershipExpiring: "Th? thành viên s?p h?t h?n"
   - FineNotification: "Thông báo ph?t"

## ?? H??ng d?n hoàn thi?n d? án

### **B??c 1: Di chuy?n entities hi?n t?i**
```bash
# Di chuy?n Models t? LibraryManagement sang LibraryManagement.Domain
# C?p nh?t namespace và inheritance t? BaseEntity
```

### **B??c 2: T?o DbContext m?i**
```csharp
// T?o LibraryDbContext trong Infrastructure layer
// Include t?t c? entities m?i
// C?u hình relationships
```

### **B??c 3: Implement Service Layer**
```bash
# T?o concrete services trong Application/Services
# Implement AutoMapper profiles
# Add validation v?i FluentValidation
```

### **B??c 4: C?p nh?t Presentation Layer**
```bash
# T?o Controllers cho entities m?i
# Add dependency injection
# Implement authentication/authorization
```

### **B??c 5: Database Migration**
```bash
dotnet ef migrations add AddNewEntities --project LibraryManagement.Infrastructure
dotnet ef database update --project LibraryManagement.Infrastructure
```

## ?? Next Steps Implementation

### **Service Implementation Example:**
```csharp
public class MemberVisitService : IMemberVisitService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public async Task<MemberVisitDto> CheckInAsync(CreateMemberVisitDto createDto)
    {
        // Implementation here
    }
}
```

### **Controller Implementation Example:**
```csharp
[ApiController]
[Route("api/[controller]")]
public class MemberVisitsController : ControllerBase
{
    private readonly IMemberVisitService _memberVisitService;
    
    [HttpPost("checkin")]
    public async Task<ActionResult<MemberVisitDto>> CheckIn(CreateMemberVisitDto dto)
    {
        // Implementation here
    }
}
```

## ?? Tính n?ng m? r?ng ???c ?? xu?t

1. **Real-time notifications** v?i SignalR
2. **QR Code scanning** cho check-in/out
3. **Mobile app support** v?i responsive APIs
4. **Advanced reporting** v?i dashboard
5. **Integration** v?i h? th?ng thanh toán
6. **Smart scheduling** cho rooms và assets
7. **AI-powered recommendations** cho books
8. **IoT integration** cho asset monitoring

## ??? Architecture Benefits

- **Separation of Concerns**: M?i layer có trách nhi?m riêng bi?t
- **Dependency Inversion**: High-level modules không ph? thu?c vào low-level
- **Testability**: D? dàng unit test và integration test
- **Maintainability**: Code d? maintain và extend
- **Scalability**: Có th? scale t?ng layer ??c l?p
- **Reusability**: Generic repository có th? tái s? d?ng

D? án ?ã ???c thi?t k? theo Clean Architecture v?i ??y ?? các layer và patterns c?n thi?t. Các entity và relationships ?ã ???c thi?t k? h?p lý v?i business logic phù h?p cho h? th?ng qu?n lý th? vi?n.