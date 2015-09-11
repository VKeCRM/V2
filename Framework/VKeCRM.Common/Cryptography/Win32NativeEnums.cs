using System;
using System.Diagnostics.CodeAnalysis;

namespace VKeCRM.Common.Cryptography
{

    #region Errors Enum

    /// <summary>
    /// Represents system error codes that may be returned using native APIs. 
    /// These values are defined in the WinError.h header file.
    /// </summary>
    public enum eErrors : int
    {
        /// <summary>
        /// The operation completed successfully.
        /// </summary>
        /// 
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] NO_ERROR = 0,

        /// <summary>
        /// The operation completed successfully.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SUCCESS = 0,

        /// <summary>
        /// Incorrect function.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_FUNCTION = 1,

        /// <summary>
        /// The specified file was not found.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_FILE_NOT_FOUND = 2,

        /// <summary>
        /// The specified path was not found.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_PATH_NOT_FOUND = 3,

        /// <summary>
        /// The system cannot open the file.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_TOO_MANY_OPEN_FILES = 4,

        /// <summary>
        /// Access to the specified file is denied.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_ACCESS_DENIED = 5,

        /// <summary>
        /// The handle is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_HANDLE = 6,

        /// <summary>
        /// The storage control blocks were destroyed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_ARENA_TRASHED = 7,

        /// <summary>
        /// There is not enough memory to perform the specified action.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NOT_ENOUGH_MEMORY = 8,

        /// <summary>
        /// The storage control block address is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_BLOCK = 9,

        /// <summary>
        /// The environment is incorrect.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_ENVIRONMENT = 10,

        /// <summary>
        /// An attempt was made to load a program with an incorrect format.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_FORMAT = 11,

        /// <summary>
        /// The access code is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_ACCESS = 12,

        /// <summary>
        /// The data is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_DATA = 13,

        /// <summary>
        /// Not enough storage is available to complete this operation.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_OUTOFMEMORY = 14,

        /// <summary>
        /// The system cannot find the drive specified.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_DRIVE = 15,

        /// <summary>
        /// The directory cannot be removed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_CURRENT_DIRECTORY = 16,

        /// <summary>
        /// The system cannot move the file to a different disk drive.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NOT_SAME_DEVICE = 17,

        /// <summary>
        /// There are no more files.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NO_MORE_FILES = 18,

        /// <summary>
        /// The media is write protected.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_WRITE_PROTECT = 19,

        /// <summary>
        /// The system cannot find the device specified.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_UNIT = 20,

        /// <summary>
        /// The device is not ready.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NOT_READY = 21,

        /// <summary>
        /// The device does not recognize the command.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_COMMAND = 22,

        /// <summary>
        /// Data error (cyclic redundancy check).
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_CRC = 23,

        /// <summary>
        /// The program issued a command but the command length is incorrect.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_LENGTH = 24,

        /// <summary>
        /// The drive cannot locate a specific area or track on the disk.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SEEK = 25,

        /// <summary>
        /// The specified disk or diskette cannot be accessed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NOT_DOS_DISK = 26,

        /// <summary>
        /// The drive cannot find the sector requested.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SECTOR_NOT_FOUND = 27,

        /// <summary>
        /// The printer is out of paper.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_OUT_OF_PAPER = 28,

        /// <summary>
        /// The system cannot write to the specified device.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_WRITE_FAULT = 29,

        /// <summary>
        /// The system cannot read from the specified device.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_READ_FAULT = 30,

        /// <summary>
        /// A device attached to the system is not functioning.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_GEN_FAILURE = 31,

        /// <summary>
        /// The process cannot access the file because it is being used by another process.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SHARING_VIOLATION = 32,

        /// <summary>
        /// The process cannot access the file because another process has locked a portion of the file.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_LOCK_VIOLATION = 33,

        /// <summary>
        /// The wrong diskette is in the drive. Insert disk into drive.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_WRONG_DISK = 34,

        /// <summary>
        /// Too many files opened for sharing.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SHARING_BUFFER_EXCEEDED = 36,

        /// <summary>
        /// Reached the end of the file.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_HANDLE_EOF = 38,

        /// <summary>
        /// The disk is full.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_HANDLE_DISK_FULL = 39,

        /// <summary>
        /// The request is not supported.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NOT_SUPPORTED = 50,

        /// <summary>
        /// Windows cannot find the network path. Verify that the network path is correct and the destination 
        /// computer is not busy or turned off. If Windows still cannot find the network path, contact your 
        /// network administrator.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_REM_NOT_LIST = 51,

        /// <summary>
        /// You were not connected because a duplicate name exists on the network. If joining a domain, go to 
        /// System in Control Panel to change the computer name and try again. If joining a workgroup, choose 
        /// another workgroup name.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DUP_NAME = 52,

        /// <summary>
        /// The network path was not found.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_NETPATH = 53,

        /// <summary>
        /// The network is busy.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NETWORK_BUSY = 54,

        /// <summary>
        /// The specified network resource or device is no longer available.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DEV_NOT_EXIST = 55,

        /// <summary>
        /// The network BIOS command limit has been reached.         
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_TOO_MANY_CMDS = 56,

        /// <summary>
        /// A network adapter hardware error occurred.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_ADAP_HDW_ERR = 57,

        /// <summary>
        /// The specified server cannot perform the requested operation.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_NET_RESP = 58,

        /// <summary>
        /// An unexpected network error occurred.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_UNEXP_NET_ERR = 59,

        /// <summary>
        /// The remote adapter is not compatible.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_REM_ADAP = 60,

        /// <summary>
        /// The printer queue is full.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_PRINTQ_FULL = 61,

        /// <summary>
        /// Space to store the file waiting to be printed is not available on the server.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NO_SPOOL_SPACE = 62,

        /// <summary>
        /// Your file waiting to be printed was deleted.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_PRINT_CANCELLED = 63,

        /// <summary>
        /// The specified network name is no longer available.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NETNAME_DELETED = 64,

        /// <summary>
        /// Network access is denied.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NETWORK_ACCESS_DENIED = 65,

        /// <summary>
        /// The network resource type is not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_DEV_TYPE = 66,

        /// <summary>
        /// The network name cannot be found.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_NET_NAME = 67,

        /// <summary>
        /// The name limit for the local computer network adapter card was exceeded.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_TOO_MANY_NAMES = 68,

        /// <summary>
        /// The network BIOS session limit was exceeded.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_TOO_MANY_SESS = 69,

        /// <summary>
        /// The remote server has been paused or is in the process of being started.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SHARING_PAUSED = 70,

        /// <summary>
        /// No more connections can be made to this remote computer at this time 
        /// because there are already as many connections as the computer can accept.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_REQ_NOT_ACCEP = 71,

        /// <summary>
        /// The specified printer or disk device has been paused.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_REDIR_PAUSED = 72,

        /// <summary>
        /// The file exists.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_FILE_EXISTS = 80,

        /// <summary>
        /// The directory or file cannot be created.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_CANNOT_MAKE = 82,

        /// <summary>
        /// Fail on INT 24.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_FAIL_I24 = 83,

        /// <summary>
        /// Storage to process this request is not available.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_OUT_OF_STRUCTURES = 84,

        /// <summary>
        /// The local device name is already in use.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_ALREADY_ASSIGNED = 85,

        /// <summary>
        /// The specified network password is not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_PASSWORD = 86,

        /// <summary>
        /// The parameter is incorrect.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_PARAMETER = 87,

        /// <summary>
        /// A write fault occurred on the network.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NET_WRITE_FAULT = 88,

        /// <summary>
        /// The system cannot start another process at this time.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NO_PROC_SLOTS = 89,

        /// <summary>
        /// Cannot create another system semaphore.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_TOO_MANY_SEMAPHORES = 100,

        /// <summary>
        /// The exclusive semaphore is owned by another process.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_EXCL_SEM_ALREADY_OWNED = 101,

        /// <summary>
        /// The semaphore is set and cannot be closed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SEM_IS_SET = 102,

        /// <summary>
        /// The semaphore cannot be set again.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_TOO_MANY_SEM_REQUESTS = 103,

        /// <summary>
        /// Cannot request exclusive semaphores at interrupt time.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_AT_INTERRUPT_TIME = 104,

        /// <summary>
        /// The previous ownership of this semaphore has ended.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SEM_OWNER_DIED = 105,

        /// <summary>
        /// Insert the diskette for drive.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SEM_USER_LIMIT = 106,

        /// <summary>
        /// The program stopped because an alternate diskette was not inserted.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DISK_CHANGE = 107,

        /// <summary>
        /// The disk is in use or locked by another process.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DRIVE_LOCKED = 108,

        /// <summary>
        /// The pipe has been ended.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BROKEN_PIPE = 109,

        /// <summary>
        /// The system cannot open the device or file specified.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_OPEN_FAILED = 110,

        /// <summary>
        /// The file name is too long.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BUFFER_OVERFLOW = 111,

        /// <summary>
        /// There is not enough space on the disk.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DISK_FULL = 112,

        /// <summary>
        /// No more internal file identifiers available.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NO_MORE_SEARCH_HANDLES = 113,

        /// <summary>
        /// The target internal file identifier is incorrect.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_TARGET_HANDLE = 114,

        /// <summary>
        /// The IOCTL call made by the application program is not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_CATEGORY = 117,

        /// <summary>
        /// The verify-on-write switch parameter value is not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_VERIFY_SWITCH = 118,

        /// <summary>
        /// The system does not support the command requested.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_DRIVER_LEVEL = 119,

        /// <summary>
        /// This function is not supported on this system.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_CALL_NOT_IMPLEMENTED = 120,

        /// <summary>
        /// The semaphore timeout period has expired.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SEM_TIMEOUT = 121,

        /// <summary>
        /// The data area passed to a system call is too small.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INSUFFICIENT_BUFFER = 122,

        /// <summary>
        /// The filename, directory name, or volume label syntax is incorrect.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_NAME = 123,

        /// <summary>
        /// The system call level is not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_LEVEL = 124,

        /// <summary>
        /// The disk has no volume label.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NO_VOLUME_LABEL = 125,

        /// <summary>
        /// The specified module could not be found.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_MOD_NOT_FOUND = 126,

        /// <summary>
        /// The specified procedure could not be found.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_PROC_NOT_FOUND = 127,

        /// <summary>
        /// There are no child processes to wait for.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_WAIT_NO_CHILDREN = 128,

        /// <summary>
        /// The application cannot be run in Win32 mode.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_CHILD_NOT_COMPLETE = 129,

        /// <summary>
        /// Attempt to use a file handle to an open disk partition for an operation other than raw disk I/O.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DIRECT_ACCESS_HANDLE = 130,

        /// <summary>
        /// An attempt was made to move the file pointer before the beginning of the file.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NEGATIVE_SEEK = 131,

        /// <summary>
        /// The file pointer cannot be set on the specified device or file.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SEEK_ON_DEVICE = 132,

        /// <summary>
        /// A JOIN or SUBST command cannot be used for a drive that contains previously joined drives.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_IS_JOIN_TARGET = 133,

        /// <summary>
        /// An attempt was made to use a JOIN or SUBST command on a drive that has already been joined.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_IS_JOINED = 134,

        /// <summary>
        /// An attempt was made to use a JOIN or SUBST command on a drive that has already been substituted.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_IS_SUBSTED = 135,

        /// <summary>
        /// The system tried to delete the JOIN of a drive that is not joined.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NOT_JOINED = 136,

        /// <summary>
        /// The system tried to delete the substitution of a drive that is not substituted.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NOT_SUBSTED = 137,

        /// <summary>
        /// The system tried to join a drive to a directory on a joined drive.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_JOIN_TO_JOIN = 138,

        /// <summary>
        /// The system tried to substitute a drive to a directory on a substituted drive.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SUBST_TO_SUBST = 139,

        /// <summary>
        /// The system tried to join a drive to a directory on a substituted drive.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_JOIN_TO_SUBST = 140,

        /// <summary>
        /// The system tried to SUBST a drive to a directory on a joined drive.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SUBST_TO_JOIN = 141,

        /// <summary>
        /// The system cannot perform a JOIN or SUBST at this time.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BUSY_DRIVE = 142,

        /// <summary>
        /// The system cannot join or substitute a drive to or for a directory on the same drive.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SAME_DRIVE = 143,

        /// <summary>
        /// The directory is not a subdirectory of the root directory.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DIR_NOT_ROOT = 144,

        /// <summary>
        /// The directory is not empty.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DIR_NOT_EMPTY = 145,

        /// <summary>
        /// The path specified is being used in a substitute.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_IS_SUBST_PATH = 146,

        /// <summary>
        /// Not enough resources are available to process this command.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_IS_JOIN_PATH = 147,

        /// <summary>
        /// The path specified cannot be used at this time.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_PATH_BUSY = 148,

        /// <summary>
        /// An attempt was made to join or substitute a drive for which a directory 
        /// on the drive is the target of a previous substitute.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_IS_SUBST_TARGET = 149,

        /// <summary>
        /// System trace information was not specified in your CONFIG.SYS file, or 
        /// tracing is disallowed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SYSTEM_TRACE = 150,

        /// <summary>
        /// The number of specified semaphore events for DosMuxSemWait is not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_EVENT_COUNT = 151,

        /// <summary>
        /// DosMuxSemWait did not execute; too many semaphores are already set.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_TOO_MANY_MUXWAITERS = 152,

        /// <summary>
        /// The DosMuxSemWait list is not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_LIST_FORMAT = 153,

        /// <summary>
        /// The volume label you entered exceeds the label character limit of the 
        /// target file system.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_LABEL_TOO_LONG = 154,

        /// <summary>
        /// Cannot create another thread.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_TOO_MANY_TCBS = 155,

        /// <summary>
        /// The recipient process has refused the signal.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SIGNAL_REFUSED = 156,

        /// <summary>
        /// The segment is already discarded and cannot be locked.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DISCARDED = 157,

        /// <summary>
        /// The segment is already unlocked.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NOT_LOCKED = 158,

        /// <summary>
        /// The address for the thread ID is not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_THREADID_ADDR = 159,

        /// <summary>
        /// One or more arguments are not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_ARGUMENTS = 160,

        /// <summary>
        /// The specified path is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_PATHNAME = 161,

        /// <summary>
        /// A signal is already pending.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SIGNAL_PENDING = 162,

        /// <summary>
        /// No more threads can be created in the system.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_MAX_THRDS_REACHED = 164,

        /// <summary>
        /// Unable to lock a region of a file.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_LOCK_FAILED = 167,

        /// <summary>
        /// The requested resource is in use.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BUSY = 170,

        /// <summary>
        /// A lock request was not outstanding for the supplied cancel region.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_CANCEL_VIOLATION = 173,

        /// <summary>
        /// The file system does not support atomic changes to the lock type.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_ATOMIC_LOCKS_NOT_SUPPORTED = 174,

        /// <summary>
        /// The system detected a segment number that was not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_SEGMENT_NUMBER = 180,

        /// <summary>
        /// The operating system cannot run item at ordinal.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_ORDINAL = 182,

        /// <summary>
        /// Cannot create a file when that file already exists.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_ALREADY_EXISTS = 183,

        /// <summary>
        /// The flag passed is not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_FLAG_NUMBER = 186,

        /// <summary>
        /// The specified system semaphore name was not found.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_SEM_NOT_FOUND = 187,

        /// <summary>
        /// The operating system cannot run item at code segment.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_STARTING_CODESEG = 188,

        /// <summary>
        /// The operating system cannot run item at stack segment.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_STACKSEG = 189,

        /// <summary>
        /// The operating system cannot run module.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_MODULETYPE = 190,

        /// <summary>
        /// Cannot run executable in Win32 mode.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_EXE_SIGNATURE = 191,

        /// <summary>
        /// The operating system cannot run application program.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_EXE_MARKED_INVALID = 192,

        /// <summary>
        /// Application is not a valid Win32 application.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_EXE_FORMAT = 193,

        /// <summary>
        /// The operating system cannot run application program.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_ITERATED_DATA_EXCEEDS_64k = 194,

        /// <summary>
        /// The operating system cannot run application program.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_MINALLOCSIZE = 195,

        /// <summary>
        /// The operating system cannot run application program.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DYNLINK_FROM_INVALID_RING = 196,

        /// <summary>
        /// The operating system is not configured to run this application program.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_IOPL_NOT_ENABLED = 197,

        /// <summary>
        /// The operating system cannot run application program.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_SEGDPL = 198,

        /// <summary>
        /// The operating system cannot run this application program.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_AUTODATASEG_EXCEEDS_64k = 199,

        /// <summary>
        /// The code segment cannot be greater than or equal to 64K.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_RING2SEG_MUST_BE_MOVABLE = 200,

        /// <summary>
        /// The operating system cannot run this application program.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_RELOC_CHAIN_XEEDS_SEGLIM = 201,

        /// <summary>
        /// The operating system cannot run this application program.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INFLOOP_IN_RELOC_CHAIN = 202,

        /// <summary>
        /// The system could not find the environment option that was entered.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_ENVVAR_NOT_FOUND = 203,

        /// <summary>
        /// No process in the command subtree has a signal handler.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NO_SIGNAL_SENT = 205,

        /// <summary>
        /// The filename or extension is too long.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_FILENAME_EXCED_RANGE = 206,

        /// <summary>
        /// The ring 2 stack is in use.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_RING2_STACK_IN_USE = 207,

        /// <summary>
        /// The global filename characters, * or ?, are entered incorrectly or too 
        /// many global filename characters are specified.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_META_EXPANSION_TOO_LONG = 208,

        /// <summary>
        /// The signal being posted is not correct.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_SIGNAL_NUMBER = 209,

        /// <summary>
        /// The signal handler cannot be set.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_THREAD_1_INACTIVE = 210,

        /// <summary>
        /// The segment is locked and cannot be reallocated.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_LOCKED = 212,

        /// <summary>
        /// Too many dynamic-link modules are attached to this program or dynamic-link module.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_TOO_MANY_MODULES = 214,

        /// <summary>
        /// Cannot nest calls to LoadModule.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NESTING_NOT_ALLOWED = 215,

        /// <summary>
        /// The application machine version is not compatible with the version you're running. 
        /// Check your computer's system information to see whether you need a x86 (32-bit) or 
        /// x64 (64-bit) version of the program, and then contact the software publisher.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_EXE_MACHINE_TYPE_MISMATCH = 216,

        /// <summary>
        /// The image file is signed, unable to modify. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_EXE_CANNOT_MODIFY_SIGNED_BINARY = 217,

        /// <summary>
        /// The image file is strong signed, unable to modify. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_EXE_CANNOT_MODIFY_STRONG_SIGNED_BINARY
            = 218,

        /// <summary>
        /// This file is checked out or locked for editing by another user.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_FILE_CHECKED_OUT = 220,

        /// <summary>
        /// The file must be checked out before saving changes. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_CHECKOUT_REQUIRED = 221,

        /// <summary>
        /// The file type being saved or retrieved has been blocked. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_FILE_TYPE = 222,

        /// <summary>
        /// The file size exceeds the limit allowed and cannot be saved. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_FILE_TOO_LARGE = 223,

        /// <summary>
        /// Access Denied. Before opening files in this location, you must first browse to 
        /// the web site and select the option to login automatically. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_FORMS_AUTH_REQUIRED = 224,

        /// <summary>
        /// Operation did not complete successfully because the file contains a virus. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_VIRUS_INFECTED = 225,

        /// <summary>
        /// This file contains a virus and cannot be opened. Due to the nature of this virus, 
        /// the file has been removed from this location. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_VIRUS_DELETED = 226,

        /// <summary>
        /// The pipe is local.     
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_PIPE_LOCAL = 229,

        /// <summary>
        /// The pipe state is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_BAD_PIPE = 230,

        /// <summary>
        /// All pipe instances are busy.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_PIPE_BUSY = 231,

        /// <summary>
        /// The pipe is being closed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NO_DATA = 232,

        /// <summary>
        /// No process is on the other end of the pipe.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_PIPE_NOT_CONNECTED = 233,

        /// <summary>
        /// More data is available.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_MORE_DATA = 234,

        /// <summary>
        /// The session was canceled.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_VC_DISCONNECTED = 240,

        /// <summary>
        /// The specified extended attribute name was invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_EA_NAME = 254,

        /// <summary>
        /// The extended attributes are inconsistent.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_EA_LIST_INCONSISTENT = 255,

        /// <summary>
        /// The wait operation timed out. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] WAIT_TIMEOUT = 258,

        /// <summary>
        /// No more data is available.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NO_MORE_ITEMS = 259,

        /// <summary>
        /// The copy functions cannot be used.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_CANNOT_COPY = 266,

        /// <summary>
        /// The directory name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DIRECTORY = 267,

        /// <summary>
        /// The extended attributes did not fit in the buffer.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_EAS_DIDNT_FIT = 275,

        /// <summary>
        /// The extended attribute file on the mounted file system is corrupt.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_EA_FILE_CORRUPT = 276,

        /// <summary>
        /// The extended attribute table file is full.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_EA_TABLE_FULL = 277,

        /// <summary>
        /// The specified extended attribute handle is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_EA_HANDLE = 278,

        /// <summary>
        /// The mounted file system does not support extended attributes.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_EAS_NOT_SUPPORTED = 282,

        /// <summary>
        /// Attempt to release mutex not owned by caller.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_NOT_OWNER = 288,

        /// <summary>
        /// Too many posts were made to a semaphore.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_TOO_MANY_POSTS = 298,

        /// <summary>
        /// Only part of a ReadProcessMemory or WriteProcessMemory request was completed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_PARTIAL_COPY = 299,

        /// <summary>
        /// The oplock request is denied.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_OPLOCK_NOT_GRANTED = 300,

        /// <summary>
        /// An invalid oplock acknowledgment was received by the system.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_INVALID_OPLOCK_PROTOCOL = 301,

        /// <summary>
        /// The volume is too fragmented to complete this operation.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DISK_TOO_FRAGMENTED = 302,

        /// <summary>
        /// The file cannot be opened because it is in the process of being deleted.  
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_DELETE_PENDING = 303,

        /// <summary>
        /// The system cannot find message text for message number in the message file.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] ERROR_MR_MID_NOT_FOUND = 317,

        /// <summary>
        /// The scope specified was not found. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SCOPE_NOT_FOUND = 318,

        /// <summary>
        /// No action was taken as a system reboot is required. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_FAIL_NOACTION_REBOOT = 350,

        /// <summary>
        ///  The shutdown operation failed. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_FAIL_SHUTDOWN = 351,

        /// <summary>
        /// The restart operation failed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_FAIL_RESTART = 352,

        /// <summary>
        /// The maximum number of sessions has been reached.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_MAX_SESSIONS_REACHED = 353,

        /// <summary>
        /// The thread is already in background processing mode.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_THREAD_MODE_ALREADY_BACKGROUND = 400,

        /// <summary>
        /// The thread is not in background processing mode. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_THREAD_MODE_NOT_BACKGROUND = 401,

        /// <summary>
        /// The process is already in background processing mode.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_PROCESS_MODE_ALREADY_BACKGROUND = 402,

        /// <summary>
        /// The process is not in background processing mode. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_PROCESS_MODE_NOT_BACKGROUND = 403,

        /// <summary>
        /// Attempt to access invalid address.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_ADDRESS = 487,

        /// <summary>
        /// User profile cannot be loaded.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_USER_PROFILE_LOAD = 500,

        /// <summary>
        /// Arithmetic result exceeded 32 bits.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_ARITHMETIC_OVERFLOW = 534,

        /// <summary>
        /// There is a process on other end of the pipe.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_PIPE_CONNECTED = 535,

        /// <summary>
        /// Waiting for a process to open the other end of the pipe.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_PIPE_LISTENING = 536,

        /// <summary>
        /// Access to the extended attribute was denied. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_EA_ACCESS_DENIED = 994,

        /// <summary>
        /// The I/O operation has been aborted because of either a thread exit or 
        /// an application request.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_OPERATION_ABORTED = 995,

        /// <summary>
        /// Overlapped I/O event is not in a signaled state.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_IO_INCOMPLETE = 996,

        /// <summary>
        /// Overlapped I/O operation is in progress.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_IO_PENDING = 997,

        /// <summary>
        /// Invalid access to memory location.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NOACCESS = 998,

        /// <summary>
        /// Error performing inpage operation.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SWAPERROR = 999,

        /// <summary>
        /// Cannot complete this function.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CAN_NOT_COMPLETE = 1003,

        /// <summary>
        /// Invalid flags.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_FLAGS = 1004,

        /// <summary>
        /// The configuration registry key is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_BADKEY = 1010,

        /// <summary>
        /// The configuration registry key could not be opened.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CANTOPEN = 1011,

        /// <summary>
        /// The configuration registry key could not be read.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CANTREAD = 1012,

        /// <summary>
        /// The configuration registry key could not be written.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CANTWRITE = 1013,

        /// <summary>
        /// One of the files in the registry database had to be recovered by use of a 
        /// log or alternate copy. The recovery was successful. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_REGISTRY_RECOVERED = 1014,

        /// <summary>
        /// The registry is corrupted. The structure of one of the files containing 
        /// registry data is corrupted, or the system's memory image of the file is 
        /// corrupted, or the file could not be recovered because the alternate copy 
        /// or log was absent or corrupted. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_REGISTRY_CORRUPT = 1015,

        /// <summary>
        /// An I/O operation initiated by the registry failed unrecoverably. The registry 
        /// could not read in, or write out, or flush, one of the files that contain the 
        /// system's image of the registry. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_REGISTRY_IO_FAILED = 1016,

        /// <summary>
        /// The system has attempted to load or restore a file into the registry, but the 
        /// specified file is not in a registry file format. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NOT_REGISTRY_FILE = 1017,

        /// <summary>
        /// Illegal operation attempted on a registry key that has been marked for deletion. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_KEY_DELETED = 1018,

        /// <summary>
        /// A dynamic link library (DLL) initialization routine failed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_DLL_INIT_FAILED = 1114,

        /// <summary>
        /// A system shutdown is in progress.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SHUTDOWN_IN_PROGRESS = 1115,

        /// <summary>
        /// Unable to abort the system shutdown because no shutdown was in progress.  
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_SHUTDOWN_IN_PROGRESS = 1116,

        /// <summary>
        /// The request could not be performed because of an I/O device error.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_IO_DEVICE = 1117,

        /// <summary>
        ///  No serial device was successfully initialized. The serial driver will unload. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SERIAL_NO_DEVICE = 1118,

        /// <summary>
        /// A potential deadlock condition has been detected.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_POSSIBLE_DEADLOCK = 1131,

        /// <summary>
        /// The specified device name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_BAD_DEVICE = 1200,

        /// <summary>
        /// The device is not currently connected but it is a remembered connection.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CONNECTION_UNAVAIL = 1201,

        /// <summary>
        /// The local device name has a remembered connection to another network resource.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_DEVICE_ALREADY_REMEMBERED = 1202,

        /// <summary>
        /// The network path was either typed incorrectly, does not exist, or the network 
        /// provider is not currently available. Please try retyping the path or contact 
        /// your network administrator.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_NET_OR_BAD_PATH = 1203,

        /// <summary>
        /// The specified network provider name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_BAD_PROVIDER = 1204,

        /// <summary>
        /// Unable to open the network connection profile.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CANNOT_OPEN_PROFILE = 1205,

        /// <summary>
        /// The network connection profile is corrupted.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_BAD_PROFILE = 1206,

        /// <summary>
        /// Cannot enumerate a noncontainer.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NOT_CONTAINER = 1207,

        /// <summary>
        /// An extended error has occurred.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_EXTENDED_ERROR = 1208,

        /// <summary>
        /// The format of the specified group name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_GROUPNAME = 1209,

        /// <summary>
        /// The format of the specified computer name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_COMPUTERNAME = 1210,

        /// <summary>
        /// The format of the specified event name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_EVENTNAME = 1211,

        /// <summary>
        /// The format of the specified domain name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_DOMAINNAME = 1212,

        /// <summary>
        /// The format of the specified service name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_SERVICENAME = 1213,

        /// <summary>
        /// The format of the specified network name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_NETNAME = 1214,

        /// <summary>
        /// The format of the specified share name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_SHARENAME = 1215,

        /// <summary>
        /// The format of the specified password is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_PASSWORDNAME = 1216,

        /// <summary>
        /// The format of the specified message name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_MESSAGENAME = 1217,

        /// <summary>
        /// The format of the specified message destination is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_MESSAGEDEST = 1218,

        /// <summary>
        /// Multiple connections to a server or shared resource by the same user, 
        /// using more than one user name, are not allowed. Disconnect all previous 
        /// connections to the server or shared resource and try again.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SESSION_CREDENTIAL_CONFLICT = 1219,

        /// <summary>
        /// An attempt was made to establish a session to a network server, but 
        /// there are already too many sessions established to that server.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_REMOTE_SESSION_LIMIT_EXCEEDED = 1220,

        /// <summary>
        /// The workgroup or domain name is already in use by another computer on 
        /// the network.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_DUP_DOMAINNAME = 1221,

        /// <summary>
        /// The network is not present or not started.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_NETWORK = 1222,

        /// <summary>
        /// The operation was canceled by the user.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CANCELLED = 1223,

        /// <summary>
        /// The requested operation cannot be performed on a file with a user-mapped 
        /// section open.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_USER_MAPPED_FILE = 1224,

        /// <summary>
        /// The remote computer refused the network connection.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CONNECTION_REFUSED = 1225,

        /// <summary>
        /// The network connection was gracefully closed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_GRACEFUL_DISCONNECT = 1226,

        /// <summary>
        /// The network transport endpoint already has an address associated with it.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_ADDRESS_ALREADY_ASSOCIATED = 1227,

        /// <summary>
        /// An address has not yet been associated with the network endpoint.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_ADDRESS_NOT_ASSOCIATED = 1228,

        /// <summary>
        /// An operation was attempted on a nonexistent network connection.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CONNECTION_INVALID = 1229,

        /// <summary>
        /// An invalid operation was attempted on an active network connection.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CONNECTION_ACTIVE = 1230,

        /// <summary>
        /// The network location cannot be reached. For information about network 
        /// troubleshooting, see Windows Help.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NETWORK_UNREACHABLE = 1231,

        /// <summary>
        /// The network location cannot be reached. For information about network 
        /// troubleshooting, see Windows Help.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_HOST_UNREACHABLE = 1232,

        /// <summary>
        /// The network location cannot be reached. For information about network 
        /// troubleshooting, see Windows Help.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_PROTOCOL_UNREACHABLE = 1233,

        /// <summary>
        /// No service is operating at the destination network endpoint on the 
        /// remote system.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_PORT_UNREACHABLE = 1234,

        /// <summary>
        /// The request was aborted.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_REQUEST_ABORTED = 1235,

        /// <summary>
        /// The network connection was aborted by the local system.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CONNECTION_ABORTED = 1236,

        /// <summary>
        /// The operation could not be completed. A retry should be performed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_RETRY = 1237,

        /// <summary>
        /// A connection to the server could not be made because the limit on 
        /// the number of concurrent connections for this account has been 
        /// reached.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CONNECTION_COUNT_LIMIT = 1238,

        /// <summary>
        /// Attempting to log in during an unauthorized time of day for this 
        /// account.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LOGIN_TIME_RESTRICTION = 1239,

        /// <summary>
        /// The account is not authorized to log in from this station.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LOGIN_WKSTA_RESTRICTION = 1240,

        /// <summary>
        /// The network address could not be used for the operation requested.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INCORRECT_ADDRESS = 1241,

        /// <summary>
        /// The service is already registered.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_ALREADY_REGISTERED = 1242,

        /// <summary>
        /// The specified service does not exist.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SERVICE_NOT_FOUND = 1243,

        /// <summary>
        /// The operation being requested was not performed because the user 
        /// has not been authenticated.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NOT_AUTHENTICATED = 1244,

        /// <summary>
        /// The operation being requested was not performed because the user 
        /// has not logged on to the network. The specified service does not 
        /// exist.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NOT_LOGGED_ON = 1245,

        /// <summary>
        /// Continue with work in progress.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CONTINUE = 1246,

        /// <summary>
        /// An attempt was made to perform an initialization operation when 
        /// initialization has already been completed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_ALREADY_INITIALIZED = 1247,

        /// <summary>
        /// No more local devices.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_MORE_DEVICES = 1248,


        /// <summary>
        /// Not all privileges or groups referenced are assigned to the caller. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NOT_ALL_ASSIGNED = 1300,

        /// <summary>
        /// Some mapping between account names and security IDs was not done.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SOME_NOT_MAPPED = 1301,

        /// <summary>
        /// No system quota limits are specifically set for this account. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_QUOTAS_FOR_ACCOUNT = 1302,

        /// <summary>
        /// No encryption key is available. A well-known encryption key was returned. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LOCAL_USER_SESSION_KEY = 1303,

        /// <summary>
        /// The password is too complex to be converted to a LAN Manager password. 
        /// The LAN Manager password returned is a NULL string. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NULL_LM_PASSWORD = 1304,

        /// <summary>
        /// The revision level is unknown. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_UNKNOWN_REVISION = 1305,

        /// <summary>
        /// Indicates two revision levels are incompatible. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_REVISION_MISMATCH = 1306,

        /// <summary>
        /// This security ID may not be assigned as the owner of this object. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_OWNER = 1307,

        /// <summary>
        /// This security ID may not be assigned as the primary group of an object. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_PRIMARY_GROUP = 1308,

        /// <summary>
        /// An attempt has been made to operate on an impersonation token by a thread 
        /// that is not currently impersonating a client. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_IMPERSONATION_TOKEN = 1309,

        /// <summary>
        /// The group may not be disabled. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CANT_DISABLE_MANDATORY = 1310,

        /// <summary>
        /// There are currently no logon servers available to service the logon request. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_LOGON_SERVERS = 1311,

        /// <summary>
        /// A specified logon session does not exist. It may already have been terminated. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_SUCH_LOGON_SESSION = 1312,

        /// <summary>
        /// A specified privilege does not exist. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_SUCH_PRIVILEGE = 1313,

        /// <summary>
        /// A required privilege is not held by the client.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_PRIVILEGE_NOT_HELD = 1314,

        /// <summary>
        /// The name provided is not a properly formed account name. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_ACCOUNT_NAME = 1315,

        /// <summary>
        /// The specified account already exists. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_USER_EXISTS = 1316,

        /// <summary>
        /// The specified account does not exist. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_SUCH_USER = 1317,

        /// <summary>
        /// The specified group already exists. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_GROUP_EXISTS = 1318,

        /// <summary>
        /// The specified group does not exist. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_SUCH_GROUP = 1319,

        /// <summary>
        ///Either the specified user account is already a member of the specified group, or the specified group cannot be deleted because it contains a member. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_MEMBER_IN_GROUP = 1320,

        /// <summary>
        /// The specified user account is not a member of the specified group account. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_MEMBER_NOT_IN_GROUP = 1321,

        /// <summary>
        /// The last remaining administration account cannot be disabled or deleted. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LAST_ADMIN = 1322,

        /// <summary>
        /// Unable to update the password. The value provided as the current password is incorrect. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_WRONG_PASSWORD = 1323,

        /// <summary>
        /// Unable to update the password. The value provided for the new password contains values that are not allowed in passwords. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_ILL_FORMED_PASSWORD = 1324,

        /// <summary>
        /// Unable to update the password. The value provided for the new password does not meet the length, complexity, or history requirements of the domain. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_PASSWORD_RESTRICTION = 1325,

        /// <summary>
        /// Logon failure: unknown user name or bad password. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LOGON_FAILURE = 1326,

        /// <summary>
        /// Logon failure: user account restriction. Possible reasons are blank passwords not allowed, logon hour restrictions, or a policy restriction has been enforced. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_ACCOUNT_RESTRICTION = 1327,

        /// <summary>
        /// Logon failure: account logon time restriction violation. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_LOGON_HOURS = 1328,

        /// <summary>
        /// Logon failure: user not allowed to log on to this computer. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_WORKSTATION = 1329,

        /// <summary>
        /// Logon failure: the specified account password has expired. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_PASSWORD_EXPIRED = 1330,

        /// <summary>
        /// Logon failure: account currently disabled. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_ACCOUNT_DISABLED = 1331,

        /// <summary>
        /// No mapping between account names and security IDs was done. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NONE_MAPPED = 1332,

        /// <summary>
        /// Too many local user identifiers (LUIDs) were requested at one time. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_TOO_MANY_LUIDS_REQUESTED = 1333,

        /// <summary>
        /// No more local user identifiers (LUIDs) are available. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LUIDS_EXHAUSTED = 1334,

        /// <summary>
        /// The subauthority part of a security ID is invalid for this particular use. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_SUB_AUTHORITY = 1335,

        /// <summary>
        /// The access control list (ACL) structure is invalid. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_ACL = 1336,

        /// <summary>
        /// The security ID structure is invalid. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_SID = 1337,

        /// <summary>
        /// The security descriptor structure is invalid. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_SECURITY_DESCR = 1338,

        /// <summary>
        /// The inherited access control list (ACL) or access control entry (ACE) could not be built. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_BAD_INHERITANCE_ACL = 1340,

        /// <summary>
        /// The server is currently disabled. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SERVER_DISABLED = 1341,

        /// <summary>
        /// The server is currently enabled. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SERVER_NOT_DISABLED = 1342,

        /// <summary>
        /// The value provided was an invalid value for an identifier authority. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_ID_AUTHORITY = 1343,

        /// <summary>
        /// No more memory is available for security information updates. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_ALLOTTED_SPACE_EXCEEDED = 1344,

        /// <summary>
        /// The specified attributes are invalid, or incompatible with the attributes for the group as a whole. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_GROUP_ATTRIBUTES = 1345,

        /// <summary>
        /// Either a required impersonation level was not provided, or the provided impersonation level is invalid. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_BAD_IMPERSONATION_LEVEL = 1346,

        /// <summary>
        /// Cannot open an anonymous level security token. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CANT_OPEN_ANONYMOUS = 1347,

        /// <summary>
        /// The validation information class requested was invalid. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_BAD_VALIDATION_CLASS = 1348,

        /// <summary>
        /// The type of the token is inappropriate for its attempted use. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_BAD_TOKEN_TYPE = 1349,

        /// <summary>
        /// Unable to perform a security operation on an object that has no associated security. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_SECURITY_ON_OBJECT = 1350,

        /// <summary>
        /// Configuration information could not be read from the domain controller, either because the machine is unavailable, or access has been denied. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CANT_ACCESS_DOMAIN_INFO = 1351,

        /// <summary>
        /// The security account manager (SAM) or local security authority (LSA) server was in the wrong state to perform the security operation. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_SERVER_STATE = 1352,

        /// <summary>
        /// The domain was in the wrong state to perform the security operation. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_DOMAIN_STATE = 1353,

        /// <summary>
        /// This operation is only allowed for the Primary Domain Controller of the domain. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_DOMAIN_ROLE = 1354,

        /// <summary>
        /// The specified domain either does not exist or could not be contacted. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_SUCH_DOMAIN = 1355,

        /// <summary>
        /// The specified domain already exists. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_DOMAIN_EXISTS = 1356,

        /// <summary>
        /// An attempt was made to exceed the limit on the number of domains per server. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_DOMAIN_LIMIT_EXCEEDED = 1357,

        /// <summary>
        /// Unable to complete the requested operation because of either a catastrophic media failure or a data structure corruption on the disk. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INTERNAL_DB_CORRUPTION = 1358,

        /// <summary>
        /// An internal error occurred. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INTERNAL_ERROR = 1359,

        /// <summary>
        /// Generic access types were contained in an access mask which should already be mapped to nongeneric types. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_GENERIC_NOT_MAPPED = 1360,

        /// <summary>
        /// A security descriptor is not in the right format (absolute or self-relative). 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_BAD_DESCRIPTOR_FORMAT = 1361,

        /// <summary>
        /// The requested action is restricted for use by logon processes only. The calling process has not registered as a logon process. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NOT_LOGON_PROCESS = 1362,

        /// <summary>
        /// Cannot start a new logon session with an ID that is already in use. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LOGON_SESSION_EXISTS = 1363,

        /// <summary>
        /// A specified authentication package is unknown. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_SUCH_PACKAGE = 1364,

        /// <summary>
        /// The logon session is not in a state that is consistent with the 
        /// requested operation. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_BAD_LOGON_SESSION_STATE = 1365,

        /// <summary>
        /// The logon session ID is already in use. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LOGON_SESSION_COLLISION = 1366,

        /// <summary>
        /// A logon request contained an invalid logon type value. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_LOGON_TYPE = 1367,

        /// <summary>
        /// Unable to impersonate using a named pipe until data has been read from 
        /// that pipe. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CANNOT_IMPERSONATE = 1368,

        /// <summary>
        /// The transaction state of a registry subtree is incompatible with the 
        /// requested operation. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_RXACT_INVALID_STATE = 1369,

        /// <summary>
        /// An internal security database corruption has been encountered. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_RXACT_COMMIT_FAILURE = 1370,

        /// <summary>
        /// Cannot perform this operation on built-in accounts. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SPECIAL_ACCOUNT = 1371,

        /// <summary>
        /// Cannot perform this operation on this built-in special group. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SPECIAL_GROUP = 1372,

        /// <summary>
        /// Cannot perform this operation on this built-in special user. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SPECIAL_USER = 1373,

        /// <summary>
        /// The user cannot be removed from a group because the group is 
        /// currently the user's primary group. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_MEMBERS_PRIMARY_GROUP = 1374,

        /// <summary>
        /// The token is already in use as a primary token. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_TOKEN_ALREADY_IN_USE = 1375,

        /// <summary>
        /// The specified local group does not exist. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_SUCH_ALIAS = 1376,

        /// <summary>
        /// The specified account name is not a member of the group. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_MEMBER_NOT_IN_ALIAS = 1377,

        /// <summary>
        /// The specified account name is already a member of the group. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_MEMBER_IN_ALIAS = 1378,

        /// <summary>
        /// The specified local group already exists. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_ALIAS_EXISTS = 1379,

        /// <summary>
        /// Logon failure: the user has not been granted the requested logon 
        /// type at this computer. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LOGON_NOT_GRANTED = 1380,

        /// <summary>
        /// The maximum number of secrets that may be stored in a single system 
        /// has been exceeded. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_TOO_MANY_SECRETS = 1381,

        /// <summary>
        /// The length of a secret exceeds the maximum length allowed. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_SECRET_TOO_LONG = 1382,

        /// <summary>
        /// The local security authority database contains an internal inconsistency. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INTERNAL_DB_ERROR = 1383,

        /// <summary>
        /// During a logon attempt, the user's security context accumulated too 
        /// many security IDs. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_TOO_MANY_CONTEXT_IDS = 1384,

        /// <summary>
        /// Logon failure: the user has not been granted the requested logon type 
        /// at this computer. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LOGON_TYPE_NOT_GRANTED = 1385,

        /// <summary>
        /// A cross-encrypted password is necessary to change a user password. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NT_CROSS_ENCRYPTION_REQUIRED = 1386,

        /// <summary>
        /// A member could not be added to or removed from the local group because 
        /// the member does not exist. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_SUCH_MEMBER = 1387,

        /// <summary>
        /// A new member could not be added to a local group because the member 
        /// has the wrong account type. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_MEMBER = 1388,

        /// <summary>
        /// Too many security IDs have been specified. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_TOO_MANY_SIDS = 1389,

        /// <summary>
        /// A cross-encrypted password is necessary to change this user password. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LM_CROSS_ENCRYPTION_REQUIRED = 1390,

        /// <summary>
        /// Indicates an ACL contains no inheritable components. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_INHERITANCE = 1391,

        /// <summary>
        /// The file or directory is corrupted and unreadable. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_FILE_CORRUPT = 1392,

        /// <summary>
        /// The disk structure is corrupted and unreadable. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_DISK_CORRUPT = 1393,

        /// <summary>
        /// There is no user session key for the specified logon session. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NO_USER_SESSION_KEY = 1394,

        /// <summary>
        /// The service being accessed is licensed for a particular number of 
        /// connections. No more connections can be made to the service at this 
        /// time because there are already as many connections as the service 
        /// can accept. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_LICENSE_QUOTA_EXCEEDED = 1395,

        /// <summary>
        /// Logon Failure: The target account name is incorrect. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_WRONG_TARGET_NAME = 1396,

        /// <summary>
        /// Mutual Authentication failed. The server's password is out of date at 
        /// the domain controller. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_MUTUAL_AUTH_FAILED = 1397,

        /// <summary>
        /// There is a time and/or date difference between the client and server. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_TIME_SKEW = 1398,

        /// <summary>
        /// This operation cannot be performed on the current domain. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CURRENT_DOMAIN_NOT_ALLOWED = 1399,

        /// <summary>
        /// Invalid window handle.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_WINDOW_HANDLE = 1400,

        /// <summary>
        /// Cannot find window class.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CANNOT_FIND_WND_CLASS = 1407,

        /// <summary>
        /// Invalid window; it belongs to other thread.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_WINDOW_OF_OTHER_THREAD = 1408,

        /// <summary>
        /// Class already exists.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CLASS_ALREADY_EXISTS = 1410,

        /// <summary>
        /// Class does not exist.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CLASS_DOES_NOT_EXIST = 1411,

        /// <summary>
        /// Class still has open windows.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_CLASS_HAS_WINDOWS = 1412,

        /// <summary>
        /// The paging file is too small for this operation to complete.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_COMMITMENT_LIMIT = 1455,

        /// <summary>
        /// The specified printer driver is already installed.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_PRINTER_DRIVER_ALREADY_INSTALLED = 1795,

        /// <summary>
        /// The specified port is unknown.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_UNKNOWN_PORT = 1796,

        /// <summary>
        /// The printer driver is unknown.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_UNKNOWN_PRINTER_DRIVER = 1797,

        /// <summary>
        /// The print processor is unknown.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_UNKNOWN_PRINTPROCESSOR = 1798,

        /// <summary>
        /// The specified separator file is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_SEPARATOR_FILE = 1799,

        /// <summary>
        /// The specified priority is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_PRIORITY = 1800,

        /// <summary>
        /// The printer name is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_PRINTER_NAME = 1801,

        /// <summary>
        /// The printer already exists.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_PRINTER_ALREADY_EXISTS = 1802,

        /// <summary>
        /// The printer command is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_PRINTER_COMMAND = 1803,

        /// <summary>
        /// The specified datatype is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_DATATYPE = 1804,

        /// <summary>
        /// The environment specified is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_INVALID_ENVIRONMENT = 1805,

        /// <summary>
        /// The specified username is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_BAD_USERNAME = 2202,

        /// <summary>
        /// This network connection does not exist.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_NOT_CONNECTED = 2250,

        /// <summary>
        /// This network connection has files open or requests pending.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_OPEN_FILES = 2401,

        /// <summary>
        /// Active connections still exist.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_ACTIVE_CONNECTIONS = 2402,

        /// <summary>
        /// The device is in use by an active process and cannot be disconnected.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] ERROR_DEVICE_IN_USE = 2404,


        /// <summary>
        /// The cryptography operation completed successfully.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_OP_OK = unchecked(0),

        /// <summary>
        /// The CSP context that was specified when the hash object was created cannot be found.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_UID = unchecked((int) 0x80090001L),

        /// <summary>
        /// The hash object parameter is invalid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_HASH = unchecked((int) 0x80090002L),

        /// <summary>
        /// A keyed hash algorithm is being used, but the session key is no longer valid. 
        /// This error will be generated if the session key is destroyed before the hashing 
        /// operation is complete.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_KEY = unchecked((int) 0x80090003L),

        /// <summary>
        /// The length parameter has a nonzero value.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_LEN = unchecked((int) 0x80090004L),

        /// <summary>
        /// The data to be encrypted is invalid. 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_DATA = unchecked((int) 0x80090005L),

        /// <summary>
        /// The signature was not valid. This might be because the data itself has changed, 
        /// the description string did not match, or the wrong public key was specified.
        /// This error can also be returned if the hashing or signature algorithms do not 
        /// match the ones used to create the signature.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_SIGNATURE = unchecked((int) 0x80090006L),

        /// <summary>
        /// The version number of the key BLOB indicates a key BLOB version that the CSP 
        /// does not support.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_VER = unchecked((int) 0x80090007L),

        /// <summary>
        /// The handle specifies an algorithm that this CSP does not support.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_ALGID = unchecked((int) 0x80090008L),

        /// <summary>
        /// The flags parameter is nonzero.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_FLAGS = unchecked((int) 0x80090009L),

        /// <summary>
        /// The key BLOB type is not supported by this CSP and is possibly not valid.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_TYPE = unchecked((int) 0x8009000AL),

        /// <summary>
        /// You do not have permission to export the key. That is, when the key was created, 
        /// the <c>CRYPT_EXPORTABLE</c> flag was not specified.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_KEY_STATE = unchecked((int) 0x8009000BL),

        /// <summary>
        /// An attempt was made to add data to a hash object that is already marked as finished.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_HASH_STATE = unchecked((int) 0x8009000CL),

        /// <summary>
        /// A session key is being exported and the key parameter does not specify a public key.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_NO_KEY = unchecked((int) 0x8009000DL),

        /// <summary>
        /// The CSP ran out of memory during the operation.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_NO_MEMORY = unchecked((int) 0x8009000EL),

        /// <summary>
        /// The key container already exists, but you are attempting to create it. 
        /// If a previous attempt to open the key failed with <c>NTE_BAD_KEYSET</c>, it 
        /// implies that access to the key container is denied.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_EXISTS = unchecked((int) 0x8009000FL),

        /// <summary>
        /// An attempt was made to create a key pair when <c>CRYPT_VERIFYCONTEXT</c> was specified.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_PERM = unchecked((int) 0x80090010L),

        /// <summary>
        /// Object was not found.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_NOT_FOUND = unchecked((int) 0x80090011L),

        /// <summary>
        /// The application attempted to encrypt the same data twice.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_DOUBLE_ENCRYPT = unchecked((int) 0x80090012L),

        /// <summary>
        /// Invalid provider specified.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_PROVIDER = unchecked((int) 0x80090013L),

        /// <summary>
        /// Invalid provider type specified.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_PROV_TYPE = unchecked((int) 0x80090014L),

        /// <summary>
        /// Invalid provider public key.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_PUBLIC_KEY = unchecked((int) 0x80090015L),

        /// <summary>
        /// Unable to retrieve keyset. The following are possible causes: 
        /// <list type="bullet">
        /// <item><description>Key container does not exist.</description></item>
        /// <item><description>You do not have access to the key container.</description></item>
        /// <item><description>The Protected Storage Service is not running.</description></item>
        /// </list>   
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_KEYSET = unchecked((int) 0x80090016L),

        /// <summary>
        /// Provider type not defined.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_PROV_TYPE_NOT_DEF = unchecked((int) 0x80090017L),

        /// <summary>
        /// Invalid registration for provider type.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_PROV_TYPE_ENTRY_BAD = unchecked((int) 0x80090018L),

        /// <summary>
        /// The Crypto Service Provider (CSP) may not be set up correctly. Use of Regsvr32.exe 
        /// on CSP DLLs (Rsabase.dll or Rsaenh.dll) may fix the problem, depending on the 
        /// provider being used.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_KEYSET_NOT_DEF = unchecked((int) 0x80090019L),

        /// <summary>
        /// Invalid keyset registration.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_KEYSET_ENTRY_BAD = unchecked((int) 0x8009001AL),

        /// <summary>
        /// Provider type does not match registered value.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_PROV_TYPE_NO_MATCH = unchecked((int) 0x8009001BL),

        /// <summary>
        /// Corrupt digital signature file.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_SIGNATURE_FILE_BAD = unchecked((int) 0x8009001CL),

        /// <summary>
        /// Provider DLL failed to initialize correctly.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_PROVIDER_DLL_FAIL = unchecked((int) 0x8009001DL),

        /// <summary>
        /// Provider DLL not found.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_PROV_DLL_NOT_FOUND = unchecked((int) 0x8009001EL),

        /// <summary>
        /// Invalid keyset parameter.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_BAD_KEYSET_PARAM = unchecked((int) 0x8009001FL),

        /// <summary>
        /// The function failed in some unexpected way.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_FAIL = unchecked((int) 0x80090020L),

        /// <summary>
        /// Base error occurred.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] NTE_SYS_ERR = unchecked((int) 0x80090021L)
    }

    #endregion // Errors Enum

    #region ShOPType Enum

    /// <summary>
    /// Represents the type of shell object being browsed.
    /// </summary>
    /// <seealso cref="Win32Native.SHObjectProperties"/>
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32")]
    public enum eShOPType : uint
    {
        /// <summary>
        /// Printer friendly name      
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] SHOP_PRINTERNAME = 0x00000001,

        /// <summary>
        /// Fully qualified path+file name      
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] SHOP_FILEPATH = 0x00000002,

        /// <summary>
        /// Volume GUID    
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] SHOP_VOLUMEGUID = 0x00000004
    } ;

    #endregion // ShOPType Enum

    #region ProvType Enum

    /// <summary>
    /// The field of cryptography is large and growing. There are many different standard 
    /// data formats and protocols. These are generally organized into groups or families, 
    /// each of which has its own set of data formats and way of doing things. Even if two 
    /// families use the same algorithm (for example, the RC2 block cipher), they will 
    /// often use different padding schemes, different key lengths, and different default 
    /// modes. CryptoAPI is designed so that a CSP provider type represents a particular 
    /// family.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When an application connects to a CSP of a particular type, each of the CryptoAPI 
    /// functions will, by default, operate in a way prescribed by the family that 
    /// corresponds to that CSP type. An application's choice of provider type specifies 
    /// the following items:
    /// <list type="table">
    /// <listheader><term>Item</term><description>Description</description></listheader>
    /// <item>
    /// <term>Key exchange algorithm</term>
    /// <description>
    /// Each provider type specifies one and only one key exchange algorithm. Every CSP 
    /// of a particular type must implement this algorithm. Applications specify the 
    /// key exchange algorithm to use by selecting a CSP of the appropriate provider type.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Digital signature algorithm</term>
    /// <description>
    /// Each provider type specifies one and only one digital signature algorithm. Every 
    /// CSP of a particular type must implement this algorithm. Applications specify the 
    /// digital signature algorithm to use by selecting a CSP of the appropriate provider 
    /// type.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Key BLOB formats</term>
    /// <description>
    /// The provide type determines the format of the key BLOB used to export keys from 
    /// the CSP and to import keys into a CSP.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Digital signature format</term>
    /// <description>
    /// The provider type determines the digital signature format. This ensures that a 
    /// signature produced by a CSP of a given provider type can be verified by any CSP 
    /// of the same provider type.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Session key derivation scheme</term>
    /// <description>
    /// The provider type determines the method used to derived a session key from a hash.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Key length</term>
    /// <description>
    /// Some provider types specify the length of public/private key pairs and the 
    /// session keys.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Default modes</term>
    /// <description>
    /// The provider type often specifies default modes for various options, such as 
    /// the block encryption cipher mode or the block encryption padding method.
    /// </description>
    /// </item>
    /// </list>
    /// Some advanced application might connect to more than one CSP at a time, but 
    /// most application generally use only a single CSP.
    /// </para>
    /// <para><br/></para>
    /// Even though some CSP types might be partially compatible with others, two 
    /// or more applications that need to exchange keys and encrypted messages should 
    /// use CSPs of the same type.
    /// </remarks>
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32")]
    public enum eProvType : uint
    {
        /// <summary>
        /// The <c>PROV_RSA_FULL</c> provider type supports both digital signatures and data 
        /// encryption. It is considered a general purpose CSP. The RSA public key 
        /// algorithm is used for all public key operations.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_RSA_FULL = 1,

        /// <summary>
        /// The <c>PROV_RSA_SIG</c> provider type is a subset of <c>PROV_RSA_FULL</c>. It supports 
        /// only those functions and algorithms required for hashes and digital signatures.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_RSA_SIG = 2,

        /// <summary>
        /// The <c>PROV_DSS</c> provider type, like <c>PROV_RSA_SIG</c>, only supports hashes and 
        /// digital signatures. The signature algorithm specified by the <c>PROV_DSS</c> provider 
        /// type is the Digital Signature Algorithm (DSA).
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_DSS = 3,

        /// <summary>
        /// The <c>PROV_FORTEZZA</c> provider type contains a set of cryptographic protocols and 
        /// algorithms owned by the National Institute of Standards and Technology (NIST).
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_FORTEZZA = 4,

        /// <summary>
        /// The <c>PROV_MS_EXCHANGE</c> provider type is designed for the cryptographic needs of 
        /// the Microsoft Exchange mail application and other applications compatible with Microsoft Mail.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_MS_EXCHANGE = 5,

        /// <summary>
        /// The <c>PROV_SSL</c> provider type supports the Secure Sockets Layer (SSL) protocol.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_SSL = 6,

        /// <summary>
        /// STT defined Provider
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_STT_MER = 7,

        /// <summary>
        /// STT defined Provider
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_STT_ACQ = 8,

        /// <summary>
        /// STT defined Provider
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_STT_BRND = 9,

        /// <summary>
        /// STT defined Provider
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_STT_ROOT = 10,

        /// <summary>
        /// STT defined Provider
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_STT_ISS = 11,

        /// <summary>
        /// The <c>PROV_RSA_SCHANNEL</c> provider type supports both RSA and Schannel protocols.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_RSA_SCHANNEL = 12,

        /// <summary>
        /// The <c>PROV_DSS_DH</c> provider is a superset of the PROV_DSS provider type.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_DSS_DH = 13,

        /// <summary>
        /// EC defined Provider
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_EC_ECDSA_SIG = 14,

        /// <summary>
        /// EC defined Provider
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_EC_ECNRA_SIG = 15,

        /// <summary>
        /// EC defined Provider
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_EC_ECDSA_FULL = 16,

        /// <summary>
        /// EC defined Provider
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_EC_ECNRA_FULL = 17,

        /// <summary>
        /// SPYRUS defined Provider
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PROV_SPYRUS_LYNKS = 20
    }

    #endregion // ProvType Enum

    #region ProvParam Enum

    /// <summary>
    /// Specifies the nature of the CryptGetProvParamAnsiString query. The following table shows defined queries.<br/>
    /// </summary>
    /// <seealso cref="Win32Native.CryptGetProvParamAnsiString"/>
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32")]
    public enum eProvParam : uint
    {
        /// <summary>
        /// A <c>PROV_ENUMALGS</c> structure containing information about one algorithm supported by the CSP being 
        /// queried. <c>PP_ENUMALGS</c> values must be read repeatedly to list all of the supported algorithms. The 
        /// algorithms are not enumerated in any particular order. The first time that the <c>PP_ENUMALGS</c> value 
        /// is read, the <c>CRYPT_FIRST</c> flag must be specified in <c>dwFlags</c>. Doing so ensures that information 
        /// about the first algorithm in the enumeration list is returned. The <c>PP_ENUMALGS</c> value can then be 
        /// repeatedly read with dwFlags set to 0 to retrieve the information about the rest of the supported 
        /// algorithms. When this function fails with the <c>ERROR_NO_MORE_ITEMS</c> error code, the end of the 
        /// enumeration list has been reached.<br/> 
        /// <br/>
        /// This function is not thread safe and all of the available algorithms might not be enumerated if 
        /// this function is used in a multithread context.<br/>
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PP_ENUMALGS = 1,

        /// <summary>
        /// Name of one of the key containers maintained by the CSP in the form of a wide-character string. 
        /// The <c>PP_ENUMCONTAINERS</c> value must be read repeatedly to enumerate all the container names. 
        /// Container names are enumerated in the same way as the CSP's supported algorithms. All of the 
        /// available container names might not be enumerated if this function is used in a multithread 
        /// context.<br/> 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PP_ENUMCONTAINERS = 2,

        /// <summary>
        /// <c>DWORD</c> value that indicates CSP implementation. Common values are:
        /// <list type="bullet">
        ///   <item><term><c>CRYPT_IMPL_HARDWARE</c></term></item>
        ///   <item><term><c>CRYPT_IMPL_SOFTWARE</c></term></item>
        ///   <item><term><c>CRYPT_IMPL_MIXED</c></term></item>
        ///   <item><term><c>CRYPT_IMPL_UNKNOWN</c></term></item>
        ///   <item><term><c>CRYPT_IMPL_REMOVABLE</c></term></item>
        /// </list>
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PP_IMPTYPE = 3,

        /// <summary>
        /// The name of the CSP in the form of a wide-character string. This string is identical to the 
        /// one passed in the <c>pszProvider</c> parameter of the <c>CryptAcquireContext</c> function to specify that 
        /// the current CSP be used.<br/>
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PP_NAME = 4,

        /// <summary>
        /// The version number of the CSP. The least significant byte contains the minor version number and 
        /// the next most significant byte the major version number. Version 1.0 is represented as 0x00000100. 
        /// To maintain backward compatibility with earlier versions of the Microsoft Base Cryptographic 
        /// Provider and the Microsoft Enhanced Cryptographic Provider, the provider names retain the v1.0 
        /// designation in later versions.<br/>
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PP_VERSION = 5,

        /// <summary>
        /// Name of the current key container as a wide-character string. This string is exactly the same as the 
        /// one passed in the <c>pszContainer</c> parameter of the <c>CryptAcquireContext</c> function to specify the key 
        /// container to use. The <c>pszContainer</c> parameter can be read to determine the name of the 
        /// default key container.<br/>
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PP_CONTAINER = 6,

        /// <summary>
        /// <c>DWORD</c> value that indicates the provider type of the CSP.<br/>
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PP_PROVTYPE = 16,

        /// <summary>
        /// <c>PROV_ENUMALGS_EX</c> structure containing information about one algorithm supported by the CSP being 
        /// queried. The structure returned contains more information about the algorithm than the structure 
        /// returned for <c>PP_ENUMALGS</c>.<br/> 
        /// <br/>
        /// The <c>PP_ENUMALGS_EX</c> value must be read repeatedly to enumerate all the supported algorithms. 
        /// The algorithms are not enumerated in any particular order. The first time that the <c>PP_ENUMALGS_EX</c> 
        /// value is read, the <c>CRYPT_FIRST</c> flag must be specified in <c>dwFlags</c>. Doing so ensures that information 
        /// about the first algorithm in the enumeration list is returned. The <c>PP_ENUMALGS_EX</c> value can 
        /// then be repeatedly read with <c>dwFlags</c> set to 0 to retrieve the information about the rest of the 
        /// supported algorithms. When this function fails with the <c>ERROR_NO_MORE_ITEMS</c> error code, the end of 
        /// the enumeration list has been reached.<br/> 
        /// <br/>
        /// This function is not thread safe and all of the available algorithms might not be enumerated if this 
        /// function is used in a multithread context.<br/> 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PP_ENUMALGS_EX = 22,

        /// <summary>
        /// The number of bits for the increment length of <c>AT_SIGNATURE</c>. This information is used with information 
        /// returned in the <c>PP_ENUMALGS_EX</c> value. With the information returned when using <c>PP_ENUMALGS_EX</c> and 
        /// <c>PP_SIG_KEYSIZE_INC</c>, the valid key lengths for <c>AT_SIGNATURE</c> can be determined. These key lengths can 
        /// then be used with <c>CryptGenKey</c>.<br/> 
        /// <br/>
        /// For example, if a CSP enumerates <c>CALG_RSA_SIGN</c> (<c>AT_SIGNATURE</c>) with a minimum key length of 512 bits 
        /// and a maximum of 1024 bits, and returns the increment length as 64 bits, then valid key lengths are 
        /// 512, 576, 640, ... 1024.<br/> 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PP_SIG_KEYSIZE_INC = 34,

        /// <summary>
        /// The number of bits for the increment length of <c>AT_KEYEXCHANGE</c>. This information is used with information 
        /// returned in the <c>PP_ENUMALGS_EX</c> value. With the information returned when using <c>PP_ENUMALGS_EX</c> and 
        /// <c>PP_KEYX_KEYSIZE_INC</c>, the valid key lengths for <c>AT_KEYEXCHANGE</c> can be determined. These key lengths can 
        /// then be used with <c>CryptGenKey</c>.<br/> 
        /// <br/>
        /// For example if a CSP enumerates <c>CALG_RSA_KEYX</c> (<c>AT_KEYEXCHANGE</c>) with a 
        /// minimum key length of 512 bits and a maximum of 1024 bits, and returns the increment length as 64 bits, 
        /// then valid key lengths are 512, 576, 640, ... 1024.<br/> 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PP_KEYX_KEYSIZE_INC = 35,

        /// <summary>
        /// Returns information on the key specifiers the CSP supports. Key specifier values are joined in a 
        /// logical <see langword="or"/> and returned as a <c>DWORD</c> value in <c>pbData</c>. For example, the Microsoft 
        /// Base Cryptographic Provider version 1.0 returns a <c>DWORD</c> value of <c>AT_SIGNATURE</c> <see langword="|"/> 
        /// <c>AT_KEYEXCHANGE</c>.<br/> 
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId = "Member")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")] PP_KEYSPEC = 39
    }

    #endregion // ProvParam Enum

    #region CryptFlags Enum

    /// <summary>
    /// Flags used to create and destroy key containers in both the current user and 
    /// machine stores.
    /// </summary>
    /// <remarks>
    /// <b>Note:</b> When key containers are created, most CSPs will not automatically 
    /// create any public/private key pairs. These keys must be created as a separate 
    /// step with the <c>CryptGenKey</c> function.
    /// </remarks>
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32"), SuppressMessage("Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags"), Flags()]
    public enum eCryptFlags : uint
    {
        /// <summary>
        /// If this flag is set, then the application will have no access to the private keys 
        /// and the container parameter must be set to
        /// <see langword="null"/>.
        /// <para><br/></para>
        /// This option is intended to be used with applications that will not be using private 
        /// keys.
        /// <para><br/></para>
        ///  When <c>CryptAcquireContext</c> is called, many CSPs will require input from the 
        /// owning user before granting access to the private keys in the key container. For 
        /// example, the private keys may be encrypted, requiring a password from the user 
        /// before they can be used. However, if the <c>CRYPT_VERIFYCONTEXT</c> flag is specified, 
        /// access to the private keys is not required and the user interface can be bypassed.
        /// </summary>                                                                                          
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] CRYPT_VERIFYCONTEXT = 0xF0000000,

        /// <summary>
        /// If this flag is set, then a new key container will be created with the name specified 
        /// by the container parameter. If container parameter is <see langword="null"/>, then a 
        /// key container with the default name will be created. See <c>CRYPT_MACHINE_KEYSET</c> 
        /// for information on combining flags.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] CRYPT_NEWKEYSET = 0x00000008,

        /// <summary>
        /// If this flag is set, then the key container specified by container parameter is 
        /// deleted. If container parameter is <see langword="null"/>, then the key container 
        /// with the default name is deleted. All key pairs in the key container are also 
        /// destroyed. 
        /// <para><br/></para>
        /// When the <c>CRYPT_DELETEKEYSET</c> flag is set, the value returned in the provider
        /// parameter is undefined and, thus, the <c>CryptReleaseContext</c> function need not 
        /// be called afterwards.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] CRYPT_DELETEKEYSET = 0x00000010,

        /// <summary>
        /// By default, key containers and their keys are stored as keys associated with a 
        /// specific user. For the CSP currently provided by Microsoft, user key containers 
        /// are stored in the user's profile. The <c>CRYPT_MACHINE_KEYSET</c> flag is useful 
        /// when access is being performed from a service or user account that did not log on 
        /// interactively.
        /// The <c>CRYPT_MACHINE_KEYSET</c> flag can be combined with other flags indicating 
        /// that the key container of interest is a machine key container and the CSP will 
        /// treat it as such. For Microsoft CSPs, this means the keys are stored locally on 
        /// the computer that created the key container.
        /// <para><br/></para>
        /// The <c>CRYPT_MACHINE_KEYSET</c> flag is used for keys that are machine keys, keys 
        /// that must be accessible to applications not associated with a user account. UI 
        /// cannot be associated with these keys because they can be used by unattended server 
        /// applications. With Microsoft CSPs, ACLs can be set on these keys by using 
        /// <c>CPGetProvParam</c> and <c>CPSetProvParam</c> with the <c>PP_KEYSET_SEC_DESCR</c> 
        /// value. CSP writers can consider setting ACLs on keys as well.
        /// <para><br/></para>
        /// If a key container is to be a machine key container, the <c>CRYPT_MACHINE_KEYSET</c> 
        /// flag must be used with all calls to <c>CryptAcquireContext</c> that reference the 
        /// container.
        /// <para><br/></para>
        /// <c>CRYPT_MACHINE_KEYSET</c> applies specifically to the Microsoft provider types. 
        /// For a list of these types, see Cryptographic Provider Types. Other provider types 
        /// can store keys differently, and can ignore this flag. All compatible CSPs must 
        /// implement the <c>CRYPT_MACHINE_KEYSET</c> flag; however, this implementation can 
        /// be done by simply ignoring the flag.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] CRYPT_MACHINE_KEYSET = 0x00000020,

        /// <summary>
        /// This flag directs the CSP to not display user interface (UI) for this context. If 
        /// any future function call using this acquired CSP context requires UI to operate, 
        /// the function call must fail and the function must set the last error to 
        /// <c>NTE_SILENT_CONTEXT</c>. If <c>CRYPT_SILENT</c> is set and a later call is made to 
        /// <c>CPGenKey</c> using <c>CRYPT_USER_PROTECTED</c>, that call will fail and the 
        /// function must set the last error to <c>NTE_SILENT_CONTEXT</c>.
        /// <para><br/></para>
        /// This flag is intended for use with applications that cannot tolerate UI being 
        /// displayed by the CSP.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] CRYPT_SILENT = 0x00000040
    }

    #endregion // CryptFlags Enum

    #region CryptPosition Enum

    /// <summary>
    /// Container Enumeration Positions
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32")]
    public enum eCryptPosition : uint
    {
        /// <summary>
        /// Retrieve first element in the enumeration.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] CRYPT_FIRST = 1,

        /// <summary>
        /// Retrieve next element in the enumeration.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")] [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")] CRYPT_NEXT = 2
    }

    #endregion // CryptPosition Enum
}