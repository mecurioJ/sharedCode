<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Management.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Management.Instrumentation.dll</Reference>
  <Namespace>System.Management</Namespace>
  <Namespace>System.Management.Instrumentation</Namespace>
</Query>

void Main()
{

var DataOutput = new List<DataItem>();

var ComputerSystem =  GetWMIProperties("Win32_ComputerSystem").Select(li => new DataItem {Name = li.Name.ToString(), Value = li.Value, Type = ((Enum)li.Type)});
var ShadowCopy =  GetWMIProperties("Win32_ShadowCopy").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
var Service =  GetWMIProperties("Win32_Service").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
var ApplicationService =  GetWMIProperties("Win32_ApplicationService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
var DependentService =  GetWMIProperties("Win32_DependentService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
var ServerFeature =  GetWMIProperties("Win32_ServerFeature").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ServiceSpecificationService =  GetWMIProperties("Win32_ServiceSpecificationService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DiskDrives = GetWMIProperties("Win32_DiskDrive").Select(li => new DataItem {Name = li.Name.ToString(), Value = li.Value, Type = ((Enum)li.Type)});		
//var LogicalDisk =  GetWMIProperties("Win32_LogicalDisk").Select(li => new DataItem {Name = li.Name.ToString(), Value = li.Value, Type = ((Enum)li.Type)});
//var SystemOperatingSystem =  GetWMIProperties("Win32_SystemOperatingSystem").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null ).Dump();
//var SystemBIOS =  GetWMIProperties("Win32_SystemBIOS").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null ).Dump();
//=ROUND((((4293496832)/1024^4)*10000),0)&" GB"
Service.Dump();
ApplicationService.Dump();
DependentService.Dump();
ServerFeature.Dump();
ShadowCopy.Dump();
ComputerSystem.Dump();

/*
DataOutput.Add(
		ComputerSystem
		.Where(nm => (new[]{"TotalPhysicalMemory","Roles","NumberOfProcessors","NumberOfLogicalProcessors","DNSHostName","Name"}).Contains(nm.Name)).ToList());
DataOutput.Add(			
		DiskDrives
		.Where(nm => (new[]{"DeviceID","Partitions","Size"}).Contains(nm.Name)).ToList());
*/

//GetWMIProperties("Win32_DiskDrive").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null ).Dump();
//GetWMIProperties("Win32_DiskDriveToDiskPartition").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null ).Dump();

//var DiskDrive =  GetWMIProperties("Win32_DiskDrive").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DiskDrivePhysicalMedia =  GetWMIProperties("Win32_DiskDrivePhysicalMedia").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DiskDriveToDiskPartition =  GetWMIProperties("Win32_DiskDriveToDiskPartition").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );

//var DataFile =  GetWMIProperties("CIM_DataFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DirectoryContainsFile =  GetWMIProperties("CIM_DirectoryContainsFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ProcessExecutable =  GetWMIProperties("CIM_ProcessExecutable").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var VideoControllerResolution =  GetWMIProperties("CIM_VideoControllerResolution").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Providers =  GetWMIProperties("Msft_Providers").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var WmiProvider_Counters =  GetWMIProperties("Msft_WmiProvider_Counters").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareLicensingProduct =  GetWMIProperties("SoftwareLicensingProduct").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareLicensingService =  GetWMIProperties("SoftwareLicensingService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareLicensingTokenActivationLicense =  GetWMIProperties("SoftwareLicensingTokenActivationLicense").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var StdRegProv =  GetWMIProperties("StdRegProv").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var a1394Controller =  GetWMIProperties("Win32_1394Controller").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var a1394ControllerDevice =  GetWMIProperties("Win32_1394ControllerDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var AccountSID =  GetWMIProperties("Win32_AccountSID").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ActionCheck =  GetWMIProperties("Win32_ActionCheck").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ActiveRoute =  GetWMIProperties("Win32_ActiveRoute").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var AllocatedResource =  GetWMIProperties("Win32_AllocatedResource").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ApplicationCommandLine =  GetWMIProperties("Win32_ApplicationCommandLine").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ApplicationService =  GetWMIProperties("Win32_ApplicationService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var AssociatedProcessorMemory =  GetWMIProperties("Win32_AssociatedProcessorMemory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var AutochkSetting =  GetWMIProperties("Win32_AutochkSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var BaseBoard =  GetWMIProperties("Win32_BaseBoard").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Battery =  GetWMIProperties("Win32_Battery").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Binary =  GetWMIProperties("Win32_Binary").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var BindImageAction =  GetWMIProperties("Win32_BindImageAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var BIOS =  GetWMIProperties("Win32_BIOS").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var BootConfiguration =  GetWMIProperties("Win32_BootConfiguration").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Bus =  GetWMIProperties("Win32_Bus").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var CacheMemory =  GetWMIProperties("Win32_CacheMemory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var CDROMDrive =  GetWMIProperties("Win32_CDROMDrive").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var CheckCheck =  GetWMIProperties("Win32_CheckCheck").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var CIMLogicalDeviceCIMDataFile =  GetWMIProperties("Win32_CIMLogicalDeviceCIMDataFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ClassicCOMApplicationClasses =  GetWMIProperties("Win32_ClassicCOMApplicationClasses").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ClassicCOMClass =  GetWMIProperties("Win32_ClassicCOMClass").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ClassicCOMClassSetting =  GetWMIProperties("Win32_ClassicCOMClassSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ClassicCOMClassSettings =  GetWMIProperties("Win32_ClassicCOMClassSettings").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ClassInfoAction =  GetWMIProperties("Win32_ClassInfoAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ClientApplicationSetting =  GetWMIProperties("Win32_ClientApplicationSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ClusterShare =  GetWMIProperties("Win32_ClusterShare").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var CodecFile =  GetWMIProperties("Win32_CodecFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var COMApplicationSettings =  GetWMIProperties("Win32_COMApplicationSettings").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ComClassAutoEmulator =  GetWMIProperties("Win32_ComClassAutoEmulator").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ComClassEmulator =  GetWMIProperties("Win32_ComClassEmulator").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var CommandLineAccess =  GetWMIProperties("Win32_CommandLineAccess").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ComponentCategory =  GetWMIProperties("Win32_ComponentCategory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ComputerSystemProcessor =  GetWMIProperties("Win32_ComputerSystemProcessor").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ComputerSystemProduct =  GetWMIProperties("Win32_ComputerSystemProduct").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Condition =  GetWMIProperties("Win32_Condition").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ConnectionShare =  GetWMIProperties("Win32_ConnectionShare").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ControllerHasHub =  GetWMIProperties("Win32_ControllerHasHub").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var CreateFolderAction =  GetWMIProperties("Win32_CreateFolderAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var CurrentProbe =  GetWMIProperties("Win32_CurrentProbe").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DCOMApplication =  GetWMIProperties("Win32_DCOMApplication").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DCOMApplicationAccessAllowedSetting =  GetWMIProperties("Win32_DCOMApplicationAccessAllowedSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DCOMApplicationLaunchAllowedSetting =  GetWMIProperties("Win32_DCOMApplicationLaunchAllowedSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DCOMApplicationSetting =  GetWMIProperties("Win32_DCOMApplicationSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DependentService =  GetWMIProperties("Win32_DependentService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Desktop =  GetWMIProperties("Win32_Desktop").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DesktopMonitor =  GetWMIProperties("Win32_DesktopMonitor").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DeviceBus =  GetWMIProperties("Win32_DeviceBus").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DeviceMemoryAddress =  GetWMIProperties("Win32_DeviceMemoryAddress").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DfsNode =  GetWMIProperties("Win32_DfsNode").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DfsNodeTarget =  GetWMIProperties("Win32_DfsNodeTarget").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DfsTarget =  GetWMIProperties("Win32_DfsTarget").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Directory =  GetWMIProperties("Win32_Directory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DirectorySpecification =  GetWMIProperties("Win32_DirectorySpecification").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );

//var DiskPartition =  GetWMIProperties("Win32_DiskPartition").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DiskQuota =  GetWMIProperties("Win32_DiskQuota").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DisplayConfiguration =  GetWMIProperties("Win32_DisplayConfiguration").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DisplayControllerConfiguration =  GetWMIProperties("Win32_DisplayControllerConfiguration").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DMAChannel =  GetWMIProperties("Win32_DMAChannel").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DriverForDevice =  GetWMIProperties("Win32_DriverForDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var DuplicateFileAction =  GetWMIProperties("Win32_DuplicateFileAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Environment =  GetWMIProperties("Win32_Environment").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var EnvironmentSpecification =  GetWMIProperties("Win32_EnvironmentSpecification").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ExtensionInfoAction =  GetWMIProperties("Win32_ExtensionInfoAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Fan =  GetWMIProperties("Win32_Fan").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var FileSpecification =  GetWMIProperties("Win32_FileSpecification").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var FloppyController =  GetWMIProperties("Win32_FloppyController").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var FloppyDrive =  GetWMIProperties("Win32_FloppyDrive").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var FontInfoAction =  GetWMIProperties("Win32_FontInfoAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Group =  GetWMIProperties("Win32_Group").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var GroupInDomain =  GetWMIProperties("Win32_GroupInDomain").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var GroupUser =  GetWMIProperties("Win32_GroupUser").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var HeatPipe =  GetWMIProperties("Win32_HeatPipe").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var IDEController =  GetWMIProperties("Win32_IDEController").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var IDEControllerDevice =  GetWMIProperties("Win32_IDEControllerDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ImplementedCategory =  GetWMIProperties("Win32_ImplementedCategory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var InfraredDevice =  GetWMIProperties("Win32_InfraredDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var IniFileSpecification =  GetWMIProperties("Win32_IniFileSpecification").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var InstalledSoftwareElement =  GetWMIProperties("Win32_InstalledSoftwareElement").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var IP4PersistedRouteTable =  GetWMIProperties("Win32_IP4PersistedRouteTable").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var IP4RouteTable =  GetWMIProperties("Win32_IP4RouteTable").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var IRQResource =  GetWMIProperties("Win32_IRQResource").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Keyboard =  GetWMIProperties("Win32_Keyboard").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LaunchCondition =  GetWMIProperties("Win32_LaunchCondition").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LoadOrderGroup =  GetWMIProperties("Win32_LoadOrderGroup").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LoadOrderGroupServiceDependencies =  GetWMIProperties("Win32_LoadOrderGroupServiceDependencies").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LoadOrderGroupServiceMembers =  GetWMIProperties("Win32_LoadOrderGroupServiceMembers").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LocalTime =  GetWMIProperties("Win32_LocalTime").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LoggedOnUser =  GetWMIProperties("Win32_LoggedOnUser").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalDisk =  GetWMIProperties("Win32_LogicalDisk").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalDiskRootDirectory =  GetWMIProperties("Win32_LogicalDiskRootDirectory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalDiskToPartition =  GetWMIProperties("Win32_LogicalDiskToPartition").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalFileAccess =  GetWMIProperties("Win32_LogicalFileAccess").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalFileAuditing =  GetWMIProperties("Win32_LogicalFileAuditing").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalFileGroup =  GetWMIProperties("Win32_LogicalFileGroup").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalFileOwner =  GetWMIProperties("Win32_LogicalFileOwner").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalFileSecuritySetting =  GetWMIProperties("Win32_LogicalFileSecuritySetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalProgramGroup =  GetWMIProperties("Win32_LogicalProgramGroup").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalProgramGroupDirectory =  GetWMIProperties("Win32_LogicalProgramGroupDirectory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalProgramGroupItem =  GetWMIProperties("Win32_LogicalProgramGroupItem").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalProgramGroupItemDataFile =  GetWMIProperties("Win32_LogicalProgramGroupItemDataFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalShareAccess =  GetWMIProperties("Win32_LogicalShareAccess").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalShareAuditing =  GetWMIProperties("Win32_LogicalShareAuditing").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogicalShareSecuritySetting =  GetWMIProperties("Win32_LogicalShareSecuritySetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogonSession =  GetWMIProperties("Win32_LogonSession").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var LogonSessionMappedDisk =  GetWMIProperties("Win32_LogonSessionMappedDisk").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var MappedLogicalDisk =  GetWMIProperties("Win32_MappedLogicalDisk").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var MemoryArray =  GetWMIProperties("Win32_MemoryArray").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var MemoryArrayLocation =  GetWMIProperties("Win32_MemoryArrayLocation").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var MemoryDevice =  GetWMIProperties("Win32_MemoryDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var MemoryDeviceArray =  GetWMIProperties("Win32_MemoryDeviceArray").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var MemoryDeviceLocation =  GetWMIProperties("Win32_MemoryDeviceLocation").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var MIMEInfoAction =  GetWMIProperties("Win32_MIMEInfoAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var MotherboardDevice =  GetWMIProperties("Win32_MotherboardDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var MountPoint =  GetWMIProperties("Win32_MountPoint").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var MoveFileAction =  GetWMIProperties("Win32_MoveFileAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NamedJobObject =  GetWMIProperties("Win32_NamedJobObject").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NamedJobObjectActgInfo =  GetWMIProperties("Win32_NamedJobObjectActgInfo").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NamedJobObjectLimit =  GetWMIProperties("Win32_NamedJobObjectLimit").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NamedJobObjectLimitSetting =  GetWMIProperties("Win32_NamedJobObjectLimitSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NamedJobObjectProcess =  GetWMIProperties("Win32_NamedJobObjectProcess").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NamedJobObjectSecLimit =  GetWMIProperties("Win32_NamedJobObjectSecLimit").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NamedJobObjectSecLimitSetting =  GetWMIProperties("Win32_NamedJobObjectSecLimitSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NamedJobObjectStatistics =  GetWMIProperties("Win32_NamedJobObjectStatistics").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NetworkAdapter =  GetWMIProperties("Win32_NetworkAdapter").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NetworkAdapterConfiguration =  GetWMIProperties("Win32_NetworkAdapterConfiguration").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NetworkAdapterSetting =  GetWMIProperties("Win32_NetworkAdapterSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NetworkClient =  GetWMIProperties("Win32_NetworkClient").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NetworkConnection =  GetWMIProperties("Win32_NetworkConnection").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NetworkLoginProfile =  GetWMIProperties("Win32_NetworkLoginProfile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NetworkProtocol =  GetWMIProperties("Win32_NetworkProtocol").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NTDomain =  GetWMIProperties("Win32_NTDomain").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NTEventlogFile =  GetWMIProperties("Win32_NTEventlogFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NTLogEvent =  GetWMIProperties("Win32_NTLogEvent").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NTLogEventComputer =  GetWMIProperties("Win32_NTLogEventComputer").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NTLogEventLog =  GetWMIProperties("Win32_NTLogEventLog").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var NTLogEventUser =  GetWMIProperties("Win32_NTLogEventUser").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ODBCAttribute =  GetWMIProperties("Win32_ODBCAttribute").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ODBCDataSourceAttribute =  GetWMIProperties("Win32_ODBCDataSourceAttribute").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ODBCDataSourceSpecification =  GetWMIProperties("Win32_ODBCDataSourceSpecification").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ODBCDriverAttribute =  GetWMIProperties("Win32_ODBCDriverAttribute").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ODBCDriverSoftwareElement =  GetWMIProperties("Win32_ODBCDriverSoftwareElement").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ODBCDriverSpecification =  GetWMIProperties("Win32_ODBCDriverSpecification").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ODBCSourceAttribute =  GetWMIProperties("Win32_ODBCSourceAttribute").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ODBCTranslatorSpecification =  GetWMIProperties("Win32_ODBCTranslatorSpecification").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var OnBoardDevice =  GetWMIProperties("Win32_OnBoardDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var OperatingSystem =  GetWMIProperties("Win32_OperatingSystem").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var OperatingSystemAutochkSetting =  GetWMIProperties("Win32_OperatingSystemAutochkSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var OperatingSystemQFE =  GetWMIProperties("Win32_OperatingSystemQFE").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var OptionalFeature =  GetWMIProperties("Win32_OptionalFeature").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var OSRecoveryConfiguration =  GetWMIProperties("Win32_OSRecoveryConfiguration").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PageFile =  GetWMIProperties("Win32_PageFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PageFileElementSetting =  GetWMIProperties("Win32_PageFileElementSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PageFileSetting =  GetWMIProperties("Win32_PageFileSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PageFileUsage =  GetWMIProperties("Win32_PageFileUsage").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ParallelPort =  GetWMIProperties("Win32_ParallelPort").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Patch =  GetWMIProperties("Win32_Patch").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PatchFile =  GetWMIProperties("Win32_PatchFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PatchPackage =  GetWMIProperties("Win32_PatchPackage").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PCMCIAController =  GetWMIProperties("Win32_PCMCIAController").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_ASPNET_ASPNET =  GetWMIProperties("Win32_PerfFormattedData_ASPNET_ASPNET").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_ASPNET_ASPNETApplications =  GetWMIProperties("Win32_PerfFormattedData_ASPNET_ASPNETApplications").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_ASPNET4030319_ASPNETAppsv4030319 =  GetWMIProperties("Win32_PerfFormattedData_ASPNET4030319_ASPNETAppsv4030319").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_ASPNET4030319_ASPNETv4030319 =  GetWMIProperties("Win32_PerfFormattedData_ASPNET4030319_ASPNETv4030319").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_aspnetstate_ASPNETStateService =  GetWMIProperties("Win32_PerfFormattedData_aspnetstate_ASPNETStateService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_AuthorizationManager_AuthorizationManagerApplications =  GetWMIProperties("Win32_PerfFormattedData_AuthorizationManager_AuthorizationManagerApplications").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_BITS_BITSNetUtilization =  GetWMIProperties("Win32_PerfFormattedData_BITS_BITSNetUtilization").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_EventTracingforWindows =  GetWMIProperties("Win32_PerfFormattedData_Counters_EventTracingforWindows").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_EventTracingforWindowsSession =  GetWMIProperties("Win32_PerfFormattedData_Counters_EventTracingforWindowsSession").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_GenericIKEv1AuthIPandIKEv2 =  GetWMIProperties("Win32_PerfFormattedData_Counters_GenericIKEv1AuthIPandIKEv2").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_HTTPService =  GetWMIProperties("Win32_PerfFormattedData_Counters_HTTPService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_HTTPServiceRequestQueues =  GetWMIProperties("Win32_PerfFormattedData_Counters_HTTPServiceRequestQueues").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_HTTPServiceUrlGroups =  GetWMIProperties("Win32_PerfFormattedData_Counters_HTTPServiceUrlGroups").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_IPHTTPSGlobal =  GetWMIProperties("Win32_PerfFormattedData_Counters_IPHTTPSGlobal").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_IPHTTPSSession =  GetWMIProperties("Win32_PerfFormattedData_Counters_IPHTTPSSession").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_IPsecAuthIPIPv4 =  GetWMIProperties("Win32_PerfFormattedData_Counters_IPsecAuthIPIPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_IPsecAuthIPIPv6 =  GetWMIProperties("Win32_PerfFormattedData_Counters_IPsecAuthIPIPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_IPsecDoSProtection =  GetWMIProperties("Win32_PerfFormattedData_Counters_IPsecDoSProtection").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_IPsecDriver =  GetWMIProperties("Win32_PerfFormattedData_Counters_IPsecDriver").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_IPsecIKEv1IPv4 =  GetWMIProperties("Win32_PerfFormattedData_Counters_IPsecIKEv1IPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_IPsecIKEv1IPv6 =  GetWMIProperties("Win32_PerfFormattedData_Counters_IPsecIKEv1IPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_IPsecIKEv2IPv4 =  GetWMIProperties("Win32_PerfFormattedData_Counters_IPsecIKEv2IPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_IPsecIKEv2IPv6 =  GetWMIProperties("Win32_PerfFormattedData_Counters_IPsecIKEv2IPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_Netlogon =  GetWMIProperties("Win32_PerfFormattedData_Counters_Netlogon").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_PacerFlow =  GetWMIProperties("Win32_PerfFormattedData_Counters_PacerFlow").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_PacerPipe =  GetWMIProperties("Win32_PerfFormattedData_Counters_PacerPipe").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_PerProcessorNetworkActivityCycles =  GetWMIProperties("Win32_PerfFormattedData_Counters_PerProcessorNetworkActivityCycles").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_PerProcessorNetworkInterfaceCardActivity =  GetWMIProperties("Win32_PerfFormattedData_Counters_PerProcessorNetworkInterfaceCardActivity").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_ProcessorInformation =  GetWMIProperties("Win32_PerfFormattedData_Counters_ProcessorInformation").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_Synchronization =  GetWMIProperties("Win32_PerfFormattedData_Counters_Synchronization").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_TeredoClient =  GetWMIProperties("Win32_PerfFormattedData_Counters_TeredoClient").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_TeredoRelay =  GetWMIProperties("Win32_PerfFormattedData_Counters_TeredoRelay").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_TeredoServer =  GetWMIProperties("Win32_PerfFormattedData_Counters_TeredoServer").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_WFP =  GetWMIProperties("Win32_PerfFormattedData_Counters_WFP").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_WFPv4 =  GetWMIProperties("Win32_PerfFormattedData_Counters_WFPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_WFPv6 =  GetWMIProperties("Win32_PerfFormattedData_Counters_WFPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Counters_WSManQuotaStatistics =  GetWMIProperties("Win32_PerfFormattedData_Counters_WSManQuotaStatistics").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_ESENT_Database =  GetWMIProperties("Win32_PerfFormattedData_ESENT_Database").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_ESENT_DatabaseInstances =  GetWMIProperties("Win32_PerfFormattedData_ESENT_DatabaseInstances").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_ESENT_DatabaseTableClasses =  GetWMIProperties("Win32_PerfFormattedData_ESENT_DatabaseTableClasses").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_LocalSessionManager_TerminalServices =  GetWMIProperties("Win32_PerfFormattedData_LocalSessionManager_TerminalServices").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Lsa_SecurityPerProcessStatistics =  GetWMIProperties("Win32_PerfFormattedData_Lsa_SecurityPerProcessStatistics").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Lsa_SecuritySystemWideStatistics =  GetWMIProperties("Win32_PerfFormattedData_Lsa_SecuritySystemWideStatistics").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_MSDTC_DistributedTransactionCoordinator =  GetWMIProperties("Win32_PerfFormattedData_MSDTC_DistributedTransactionCoordinator").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_MSDTCBridge4000_MSDTCBridge4000 =  GetWMIProperties("Win32_PerfFormattedData_MSDTCBridge4000_MSDTCBridge4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETCLRData_NETCLRData =  GetWMIProperties("Win32_PerfFormattedData_NETCLRData_NETCLRData").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETCLRNetworking_NETCLRNetworking =  GetWMIProperties("Win32_PerfFormattedData_NETCLRNetworking_NETCLRNetworking").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETCLRNetworking4000_NETCLRNetworking4000 =  GetWMIProperties("Win32_PerfFormattedData_NETCLRNetworking4000_NETCLRNetworking4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETDataProviderforOracle_NETDataProviderforOracle =  GetWMIProperties("Win32_PerfFormattedData_NETDataProviderforOracle_NETDataProviderforOracle").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETDataProviderforSqlServer_NETDataProviderforSqlServer =  GetWMIProperties("Win32_PerfFormattedData_NETDataProviderforSqlServer_NETDataProviderforSqlServer").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETFramework_NETCLRExceptions =  GetWMIProperties("Win32_PerfFormattedData_NETFramework_NETCLRExceptions").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETFramework_NETCLRInterop =  GetWMIProperties("Win32_PerfFormattedData_NETFramework_NETCLRInterop").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETFramework_NETCLRJit =  GetWMIProperties("Win32_PerfFormattedData_NETFramework_NETCLRJit").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETFramework_NETCLRLoading =  GetWMIProperties("Win32_PerfFormattedData_NETFramework_NETCLRLoading").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETFramework_NETCLRLocksAndThreads =  GetWMIProperties("Win32_PerfFormattedData_NETFramework_NETCLRLocksAndThreads").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETFramework_NETCLRMemory =  GetWMIProperties("Win32_PerfFormattedData_NETFramework_NETCLRMemory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETFramework_NETCLRRemoting =  GetWMIProperties("Win32_PerfFormattedData_NETFramework_NETCLRRemoting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETFramework_NETCLRSecurity =  GetWMIProperties("Win32_PerfFormattedData_NETFramework_NETCLRSecurity").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_NETMemoryCache40_NETMemoryCache40 =  GetWMIProperties("Win32_PerfFormattedData_NETMemoryCache40_NETMemoryCache40").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfDisk_LogicalDisk =  GetWMIProperties("Win32_PerfFormattedData_PerfDisk_LogicalDisk").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfDisk_PhysicalDisk =  GetWMIProperties("Win32_PerfFormattedData_PerfDisk_PhysicalDisk").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfNet_Browser =  GetWMIProperties("Win32_PerfFormattedData_PerfNet_Browser").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfNet_Redirector =  GetWMIProperties("Win32_PerfFormattedData_PerfNet_Redirector").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfNet_Server =  GetWMIProperties("Win32_PerfFormattedData_PerfNet_Server").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfNet_ServerWorkQueues =  GetWMIProperties("Win32_PerfFormattedData_PerfNet_ServerWorkQueues").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfOS_Cache =  GetWMIProperties("Win32_PerfFormattedData_PerfOS_Cache").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfOS_Memory =  GetWMIProperties("Win32_PerfFormattedData_PerfOS_Memory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfOS_Objects =  GetWMIProperties("Win32_PerfFormattedData_PerfOS_Objects").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfOS_PagingFile =  GetWMIProperties("Win32_PerfFormattedData_PerfOS_PagingFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfOS_Processor =  GetWMIProperties("Win32_PerfFormattedData_PerfOS_Processor").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfOS_System =  GetWMIProperties("Win32_PerfFormattedData_PerfOS_System").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfProc_FullImage_Costly =  GetWMIProperties("Win32_PerfFormattedData_PerfProc_FullImage_Costly").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfProc_Image_Costly =  GetWMIProperties("Win32_PerfFormattedData_PerfProc_Image_Costly").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfProc_JobObject =  GetWMIProperties("Win32_PerfFormattedData_PerfProc_JobObject").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfProc_JobObjectDetails =  GetWMIProperties("Win32_PerfFormattedData_PerfProc_JobObjectDetails").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfProc_Process =  GetWMIProperties("Win32_PerfFormattedData_PerfProc_Process").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfProc_ProcessAddressSpace_Costly =  GetWMIProperties("Win32_PerfFormattedData_PerfProc_ProcessAddressSpace_Costly").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfProc_Thread =  GetWMIProperties("Win32_PerfFormattedData_PerfProc_Thread").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PerfProc_ThreadDetails_Costly =  GetWMIProperties("Win32_PerfFormattedData_PerfProc_ThreadDetails_Costly").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_PowerMeterCounter_PowerMeter =  GetWMIProperties("Win32_PerfFormattedData_PowerMeterCounter_PowerMeter").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_RemoteAccess_RASPort =  GetWMIProperties("Win32_PerfFormattedData_RemoteAccess_RASPort").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_RemoteAccess_RASTotal =  GetWMIProperties("Win32_PerfFormattedData_RemoteAccess_RASTotal").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_ServiceModel4000_ServiceModelEndpoint4000 =  GetWMIProperties("Win32_PerfFormattedData_ServiceModel4000_ServiceModelEndpoint4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_ServiceModel4000_ServiceModelOperation4000 =  GetWMIProperties("Win32_PerfFormattedData_ServiceModel4000_ServiceModelOperation4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_ServiceModel4000_ServiceModelService4000 =  GetWMIProperties("Win32_PerfFormattedData_ServiceModel4000_ServiceModelService4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_SMSvcHost4000_SMSvcHost4000 =  GetWMIProperties("Win32_PerfFormattedData_SMSvcHost4000_SMSvcHost4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Spooler_PrintQueue =  GetWMIProperties("Win32_PerfFormattedData_Spooler_PrintQueue").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_TapiSrv_Telephony =  GetWMIProperties("Win32_PerfFormattedData_TapiSrv_Telephony").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_TBS_TBScounters =  GetWMIProperties("Win32_PerfFormattedData_TBS_TBScounters").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Tcpip_ICMP =  GetWMIProperties("Win32_PerfFormattedData_Tcpip_ICMP").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Tcpip_ICMPv6 =  GetWMIProperties("Win32_PerfFormattedData_Tcpip_ICMPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Tcpip_IPv4 =  GetWMIProperties("Win32_PerfFormattedData_Tcpip_IPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Tcpip_IPv6 =  GetWMIProperties("Win32_PerfFormattedData_Tcpip_IPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Tcpip_NBTConnection =  GetWMIProperties("Win32_PerfFormattedData_Tcpip_NBTConnection").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Tcpip_NetworkInterface =  GetWMIProperties("Win32_PerfFormattedData_Tcpip_NetworkInterface").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Tcpip_TCPv4 =  GetWMIProperties("Win32_PerfFormattedData_Tcpip_TCPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Tcpip_TCPv6 =  GetWMIProperties("Win32_PerfFormattedData_Tcpip_TCPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Tcpip_UDPv4 =  GetWMIProperties("Win32_PerfFormattedData_Tcpip_UDPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_Tcpip_UDPv6 =  GetWMIProperties("Win32_PerfFormattedData_Tcpip_UDPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_TermService_TerminalServicesSession =  GetWMIProperties("Win32_PerfFormattedData_TermService_TerminalServicesSession").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_usbhub_USB =  GetWMIProperties("Win32_PerfFormattedData_usbhub_USB").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfFormattedData_WindowsWorkflowFoundation4000_WFSystemWorkflow4000 =  GetWMIProperties("Win32_PerfFormattedData_WindowsWorkflowFoundation4000_WFSystemWorkflow4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_ASPNET_ASPNET =  GetWMIProperties("Win32_PerfRawData_ASPNET_ASPNET").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_ASPNET_ASPNETApplications =  GetWMIProperties("Win32_PerfRawData_ASPNET_ASPNETApplications").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_ASPNET4030319_ASPNETAppsv4030319 =  GetWMIProperties("Win32_PerfRawData_ASPNET4030319_ASPNETAppsv4030319").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_ASPNET4030319_ASPNETv4030319 =  GetWMIProperties("Win32_PerfRawData_ASPNET4030319_ASPNETv4030319").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_aspnetstate_ASPNETStateService =  GetWMIProperties("Win32_PerfRawData_aspnetstate_ASPNETStateService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_AuthorizationManager_AuthorizationManagerApplications =  GetWMIProperties("Win32_PerfRawData_AuthorizationManager_AuthorizationManagerApplications").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_BITS_BITSNetUtilization =  GetWMIProperties("Win32_PerfRawData_BITS_BITSNetUtilization").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_EventTracingforWindows =  GetWMIProperties("Win32_PerfRawData_Counters_EventTracingforWindows").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_EventTracingforWindowsSession =  GetWMIProperties("Win32_PerfRawData_Counters_EventTracingforWindowsSession").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_GenericIKEv1AuthIPandIKEv2 =  GetWMIProperties("Win32_PerfRawData_Counters_GenericIKEv1AuthIPandIKEv2").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_HTTPService =  GetWMIProperties("Win32_PerfRawData_Counters_HTTPService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_HTTPServiceRequestQueues =  GetWMIProperties("Win32_PerfRawData_Counters_HTTPServiceRequestQueues").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_HTTPServiceUrlGroups =  GetWMIProperties("Win32_PerfRawData_Counters_HTTPServiceUrlGroups").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_IPHTTPSGlobal =  GetWMIProperties("Win32_PerfRawData_Counters_IPHTTPSGlobal").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_IPHTTPSSession =  GetWMIProperties("Win32_PerfRawData_Counters_IPHTTPSSession").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_IPsecAuthIPIPv4 =  GetWMIProperties("Win32_PerfRawData_Counters_IPsecAuthIPIPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_IPsecAuthIPIPv6 =  GetWMIProperties("Win32_PerfRawData_Counters_IPsecAuthIPIPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_IPsecDoSProtection =  GetWMIProperties("Win32_PerfRawData_Counters_IPsecDoSProtection").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_IPsecDriver =  GetWMIProperties("Win32_PerfRawData_Counters_IPsecDriver").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_IPsecIKEv1IPv4 =  GetWMIProperties("Win32_PerfRawData_Counters_IPsecIKEv1IPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_IPsecIKEv1IPv6 =  GetWMIProperties("Win32_PerfRawData_Counters_IPsecIKEv1IPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_IPsecIKEv2IPv4 =  GetWMIProperties("Win32_PerfRawData_Counters_IPsecIKEv2IPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_IPsecIKEv2IPv6 =  GetWMIProperties("Win32_PerfRawData_Counters_IPsecIKEv2IPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_Netlogon =  GetWMIProperties("Win32_PerfRawData_Counters_Netlogon").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_PacerFlow =  GetWMIProperties("Win32_PerfRawData_Counters_PacerFlow").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_PacerPipe =  GetWMIProperties("Win32_PerfRawData_Counters_PacerPipe").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_PerProcessorNetworkActivityCycles =  GetWMIProperties("Win32_PerfRawData_Counters_PerProcessorNetworkActivityCycles").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_PerProcessorNetworkInterfaceCardActivity =  GetWMIProperties("Win32_PerfRawData_Counters_PerProcessorNetworkInterfaceCardActivity").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_ProcessorInformation =  GetWMIProperties("Win32_PerfRawData_Counters_ProcessorInformation").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_Synchronization =  GetWMIProperties("Win32_PerfRawData_Counters_Synchronization").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_TeredoClient =  GetWMIProperties("Win32_PerfRawData_Counters_TeredoClient").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_TeredoRelay =  GetWMIProperties("Win32_PerfRawData_Counters_TeredoRelay").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_TeredoServer =  GetWMIProperties("Win32_PerfRawData_Counters_TeredoServer").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_WFP =  GetWMIProperties("Win32_PerfRawData_Counters_WFP").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_WFPv4 =  GetWMIProperties("Win32_PerfRawData_Counters_WFPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_WFPv6 =  GetWMIProperties("Win32_PerfRawData_Counters_WFPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Counters_WSManQuotaStatistics =  GetWMIProperties("Win32_PerfRawData_Counters_WSManQuotaStatistics").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_ESENT_Database =  GetWMIProperties("Win32_PerfRawData_ESENT_Database").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_ESENT_DatabaseInstances =  GetWMIProperties("Win32_PerfRawData_ESENT_DatabaseInstances").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_ESENT_DatabaseTableClasses =  GetWMIProperties("Win32_PerfRawData_ESENT_DatabaseTableClasses").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_LocalSessionManager_TerminalServices =  GetWMIProperties("Win32_PerfRawData_LocalSessionManager_TerminalServices").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Lsa_SecurityPerProcessStatistics =  GetWMIProperties("Win32_PerfRawData_Lsa_SecurityPerProcessStatistics").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Lsa_SecuritySystemWideStatistics =  GetWMIProperties("Win32_PerfRawData_Lsa_SecuritySystemWideStatistics").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_MSDTC_DistributedTransactionCoordinator =  GetWMIProperties("Win32_PerfRawData_MSDTC_DistributedTransactionCoordinator").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_MSDTCBridge4000_MSDTCBridge4000 =  GetWMIProperties("Win32_PerfRawData_MSDTCBridge4000_MSDTCBridge4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETCLRData_NETCLRData =  GetWMIProperties("Win32_PerfRawData_NETCLRData_NETCLRData").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETCLRNetworking_NETCLRNetworking =  GetWMIProperties("Win32_PerfRawData_NETCLRNetworking_NETCLRNetworking").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETCLRNetworking4000_NETCLRNetworking4000 =  GetWMIProperties("Win32_PerfRawData_NETCLRNetworking4000_NETCLRNetworking4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETDataProviderforOracle_NETDataProviderforOracle =  GetWMIProperties("Win32_PerfRawData_NETDataProviderforOracle_NETDataProviderforOracle").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETDataProviderforSqlServer_NETDataProviderforSqlServer =  GetWMIProperties("Win32_PerfRawData_NETDataProviderforSqlServer_NETDataProviderforSqlServer").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETFramework_NETCLRExceptions =  GetWMIProperties("Win32_PerfRawData_NETFramework_NETCLRExceptions").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETFramework_NETCLRInterop =  GetWMIProperties("Win32_PerfRawData_NETFramework_NETCLRInterop").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETFramework_NETCLRJit =  GetWMIProperties("Win32_PerfRawData_NETFramework_NETCLRJit").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETFramework_NETCLRLoading =  GetWMIProperties("Win32_PerfRawData_NETFramework_NETCLRLoading").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETFramework_NETCLRLocksAndThreads =  GetWMIProperties("Win32_PerfRawData_NETFramework_NETCLRLocksAndThreads").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETFramework_NETCLRMemory =  GetWMIProperties("Win32_PerfRawData_NETFramework_NETCLRMemory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETFramework_NETCLRRemoting =  GetWMIProperties("Win32_PerfRawData_NETFramework_NETCLRRemoting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETFramework_NETCLRSecurity =  GetWMIProperties("Win32_PerfRawData_NETFramework_NETCLRSecurity").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_NETMemoryCache40_NETMemoryCache40 =  GetWMIProperties("Win32_PerfRawData_NETMemoryCache40_NETMemoryCache40").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfDisk_LogicalDisk =  GetWMIProperties("Win32_PerfRawData_PerfDisk_LogicalDisk").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfDisk_PhysicalDisk =  GetWMIProperties("Win32_PerfRawData_PerfDisk_PhysicalDisk").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfNet_Browser =  GetWMIProperties("Win32_PerfRawData_PerfNet_Browser").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfNet_Redirector =  GetWMIProperties("Win32_PerfRawData_PerfNet_Redirector").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfNet_Server =  GetWMIProperties("Win32_PerfRawData_PerfNet_Server").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfNet_ServerWorkQueues =  GetWMIProperties("Win32_PerfRawData_PerfNet_ServerWorkQueues").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfOS_Cache =  GetWMIProperties("Win32_PerfRawData_PerfOS_Cache").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfOS_Memory =  GetWMIProperties("Win32_PerfRawData_PerfOS_Memory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfOS_Objects =  GetWMIProperties("Win32_PerfRawData_PerfOS_Objects").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfOS_PagingFile =  GetWMIProperties("Win32_PerfRawData_PerfOS_PagingFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfOS_Processor =  GetWMIProperties("Win32_PerfRawData_PerfOS_Processor").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfOS_System =  GetWMIProperties("Win32_PerfRawData_PerfOS_System").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfProc_FullImage_Costly =  GetWMIProperties("Win32_PerfRawData_PerfProc_FullImage_Costly").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfProc_Image_Costly =  GetWMIProperties("Win32_PerfRawData_PerfProc_Image_Costly").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfProc_JobObject =  GetWMIProperties("Win32_PerfRawData_PerfProc_JobObject").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfProc_JobObjectDetails =  GetWMIProperties("Win32_PerfRawData_PerfProc_JobObjectDetails").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfProc_Process =  GetWMIProperties("Win32_PerfRawData_PerfProc_Process").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfProc_ProcessAddressSpace_Costly =  GetWMIProperties("Win32_PerfRawData_PerfProc_ProcessAddressSpace_Costly").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfProc_Thread =  GetWMIProperties("Win32_PerfRawData_PerfProc_Thread").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PerfProc_ThreadDetails_Costly =  GetWMIProperties("Win32_PerfRawData_PerfProc_ThreadDetails_Costly").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_PowerMeterCounter_PowerMeter =  GetWMIProperties("Win32_PerfRawData_PowerMeterCounter_PowerMeter").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_RemoteAccess_RASPort =  GetWMIProperties("Win32_PerfRawData_RemoteAccess_RASPort").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_RemoteAccess_RASTotal =  GetWMIProperties("Win32_PerfRawData_RemoteAccess_RASTotal").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_ServiceModel4000_ServiceModelEndpoint4000 =  GetWMIProperties("Win32_PerfRawData_ServiceModel4000_ServiceModelEndpoint4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_ServiceModel4000_ServiceModelOperation4000 =  GetWMIProperties("Win32_PerfRawData_ServiceModel4000_ServiceModelOperation4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_ServiceModel4000_ServiceModelService4000 =  GetWMIProperties("Win32_PerfRawData_ServiceModel4000_ServiceModelService4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_SMSvcHost4000_SMSvcHost4000 =  GetWMIProperties("Win32_PerfRawData_SMSvcHost4000_SMSvcHost4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Spooler_PrintQueue =  GetWMIProperties("Win32_PerfRawData_Spooler_PrintQueue").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_TapiSrv_Telephony =  GetWMIProperties("Win32_PerfRawData_TapiSrv_Telephony").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_TBS_TBScounters =  GetWMIProperties("Win32_PerfRawData_TBS_TBScounters").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Tcpip_ICMP =  GetWMIProperties("Win32_PerfRawData_Tcpip_ICMP").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Tcpip_ICMPv6 =  GetWMIProperties("Win32_PerfRawData_Tcpip_ICMPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Tcpip_IPv4 =  GetWMIProperties("Win32_PerfRawData_Tcpip_IPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Tcpip_IPv6 =  GetWMIProperties("Win32_PerfRawData_Tcpip_IPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Tcpip_NBTConnection =  GetWMIProperties("Win32_PerfRawData_Tcpip_NBTConnection").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Tcpip_NetworkInterface =  GetWMIProperties("Win32_PerfRawData_Tcpip_NetworkInterface").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Tcpip_TCPv4 =  GetWMIProperties("Win32_PerfRawData_Tcpip_TCPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Tcpip_TCPv6 =  GetWMIProperties("Win32_PerfRawData_Tcpip_TCPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Tcpip_UDPv4 =  GetWMIProperties("Win32_PerfRawData_Tcpip_UDPv4").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_Tcpip_UDPv6 =  GetWMIProperties("Win32_PerfRawData_Tcpip_UDPv6").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_TermService_TerminalServicesSession =  GetWMIProperties("Win32_PerfRawData_TermService_TerminalServicesSession").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_usbhub_USB =  GetWMIProperties("Win32_PerfRawData_usbhub_USB").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PerfRawData_WindowsWorkflowFoundation4000_WFSystemWorkflow4000 =  GetWMIProperties("Win32_PerfRawData_WindowsWorkflowFoundation4000_WFSystemWorkflow4000").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PhysicalMedia =  GetWMIProperties("Win32_PhysicalMedia").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PhysicalMemory =  GetWMIProperties("Win32_PhysicalMemory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PhysicalMemoryArray =  GetWMIProperties("Win32_PhysicalMemoryArray").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PhysicalMemoryLocation =  GetWMIProperties("Win32_PhysicalMemoryLocation").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PingStatus =  GetWMIProperties("Win32_PingStatus").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PNPAllocatedResource =  GetWMIProperties("Win32_PNPAllocatedResource").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PnPDevice =  GetWMIProperties("Win32_PnPDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PnPEntity =  GetWMIProperties("Win32_PnPEntity").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PnPSignedDriver =  GetWMIProperties("Win32_PnPSignedDriver").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PnPSignedDriverCIMDataFile =  GetWMIProperties("Win32_PnPSignedDriverCIMDataFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PointingDevice =  GetWMIProperties("Win32_PointingDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PortableBattery =  GetWMIProperties("Win32_PortableBattery").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PortConnector =  GetWMIProperties("Win32_PortConnector").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PortResource =  GetWMIProperties("Win32_PortResource").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var POTSModem =  GetWMIProperties("Win32_POTSModem").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var POTSModemToSerialPort =  GetWMIProperties("Win32_POTSModemToSerialPort").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Printer =  GetWMIProperties("Win32_Printer").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PrinterConfiguration =  GetWMIProperties("Win32_PrinterConfiguration").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PrinterController =  GetWMIProperties("Win32_PrinterController").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PrinterDriver =  GetWMIProperties("Win32_PrinterDriver").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PrinterDriverDll =  GetWMIProperties("Win32_PrinterDriverDll").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PrinterSetting =  GetWMIProperties("Win32_PrinterSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PrinterShare =  GetWMIProperties("Win32_PrinterShare").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PrintJob =  GetWMIProperties("Win32_PrintJob").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Process =  GetWMIProperties("Win32_Process").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Processor =  GetWMIProperties("Win32_Processor").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Product =  GetWMIProperties("Win32_Product").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ProductCheck =  GetWMIProperties("Win32_ProductCheck").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ProductResource =  GetWMIProperties("Win32_ProductResource").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ProductSoftwareFeatures =  GetWMIProperties("Win32_ProductSoftwareFeatures").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ProgIDSpecification =  GetWMIProperties("Win32_ProgIDSpecification").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ProgramGroupContents =  GetWMIProperties("Win32_ProgramGroupContents").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Property =  GetWMIProperties("Win32_Property").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ProtocolBinding =  GetWMIProperties("Win32_ProtocolBinding").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var PublishComponentAction =  GetWMIProperties("Win32_PublishComponentAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var QuickFixEngineering =  GetWMIProperties("Win32_QuickFixEngineering").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var QuotaSetting =  GetWMIProperties("Win32_QuotaSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Refrigeration =  GetWMIProperties("Win32_Refrigeration").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Registry =  GetWMIProperties("Win32_Registry").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var RegistryAction =  GetWMIProperties("Win32_RegistryAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ReliabilityRecords =  GetWMIProperties("Win32_ReliabilityRecords").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ReliabilityStabilityMetrics =  GetWMIProperties("Win32_ReliabilityStabilityMetrics").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var RemoveFileAction =  GetWMIProperties("Win32_RemoveFileAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var RemoveIniAction =  GetWMIProperties("Win32_RemoveIniAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ReserveCost =  GetWMIProperties("Win32_ReserveCost").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ScheduledJob =  GetWMIProperties("Win32_ScheduledJob").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SCSIController =  GetWMIProperties("Win32_SCSIController").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SCSIControllerDevice =  GetWMIProperties("Win32_SCSIControllerDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SecurityDescriptorHelper =  GetWMIProperties("Win32_SecurityDescriptorHelper").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SecuritySettingOfLogicalFile =  GetWMIProperties("Win32_SecuritySettingOfLogicalFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SecuritySettingOfLogicalShare =  GetWMIProperties("Win32_SecuritySettingOfLogicalShare").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SelfRegModuleAction =  GetWMIProperties("Win32_SelfRegModuleAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SerialPort =  GetWMIProperties("Win32_SerialPort").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SerialPortConfiguration =  GetWMIProperties("Win32_SerialPortConfiguration").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SerialPortSetting =  GetWMIProperties("Win32_SerialPortSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ServerConnection =  GetWMIProperties("Win32_ServerConnection").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ServerFeature =  GetWMIProperties("Win32_ServerFeature").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ServerSession =  GetWMIProperties("Win32_ServerSession").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Service =  GetWMIProperties("Win32_Service").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ServiceControl =  GetWMIProperties("Win32_ServiceControl").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ServiceSpecification =  GetWMIProperties("Win32_ServiceSpecification").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ServiceSpecificationService =  GetWMIProperties("Win32_ServiceSpecificationService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SessionConnection =  GetWMIProperties("Win32_SessionConnection").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SessionProcess =  GetWMIProperties("Win32_SessionProcess").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShadowBy =  GetWMIProperties("Win32_ShadowBy").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShadowCopy =  GetWMIProperties("Win32_ShadowCopy").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShadowDiffVolumeSupport =  GetWMIProperties("Win32_ShadowDiffVolumeSupport").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShadowFor =  GetWMIProperties("Win32_ShadowFor").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShadowOn =  GetWMIProperties("Win32_ShadowOn").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShadowProvider =  GetWMIProperties("Win32_ShadowProvider").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShadowStorage =  GetWMIProperties("Win32_ShadowStorage").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShadowVolumeSupport =  GetWMIProperties("Win32_ShadowVolumeSupport").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Share =  GetWMIProperties("Win32_Share").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShareToDirectory =  GetWMIProperties("Win32_ShareToDirectory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShortcutAction =  GetWMIProperties("Win32_ShortcutAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShortcutFile =  GetWMIProperties("Win32_ShortcutFile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var ShortcutSAP =  GetWMIProperties("Win32_ShortcutSAP").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SID =  GetWMIProperties("Win32_SID").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareElement =  GetWMIProperties("Win32_SoftwareElement").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareElementAction =  GetWMIProperties("Win32_SoftwareElementAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareElementCheck =  GetWMIProperties("Win32_SoftwareElementCheck").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareElementCondition =  GetWMIProperties("Win32_SoftwareElementCondition").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareElementResource =  GetWMIProperties("Win32_SoftwareElementResource").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareFeature =  GetWMIProperties("Win32_SoftwareFeature").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareFeatureAction =  GetWMIProperties("Win32_SoftwareFeatureAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareFeatureCheck =  GetWMIProperties("Win32_SoftwareFeatureCheck").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareFeatureParent =  GetWMIProperties("Win32_SoftwareFeatureParent").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoftwareFeatureSoftwareElements =  GetWMIProperties("Win32_SoftwareFeatureSoftwareElements").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SoundDevice =  GetWMIProperties("Win32_SoundDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var StartupCommand =  GetWMIProperties("Win32_StartupCommand").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SubDirectory =  GetWMIProperties("Win32_SubDirectory").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemAccount =  GetWMIProperties("Win32_SystemAccount").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemBIOS =  GetWMIProperties("Win32_SystemBIOS").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemBootConfiguration =  GetWMIProperties("Win32_SystemBootConfiguration").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemDesktop =  GetWMIProperties("Win32_SystemDesktop").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemDevices =  GetWMIProperties("Win32_SystemDevices").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemDriver =  GetWMIProperties("Win32_SystemDriver").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemDriverPNPEntity =  GetWMIProperties("Win32_SystemDriverPNPEntity").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemEnclosure =  GetWMIProperties("Win32_SystemEnclosure").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemLoadOrderGroups =  GetWMIProperties("Win32_SystemLoadOrderGroups").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemNetworkConnections =  GetWMIProperties("Win32_SystemNetworkConnections").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemOperatingSystem =  GetWMIProperties("Win32_SystemOperatingSystem").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemPartitions =  GetWMIProperties("Win32_SystemPartitions").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemProcesses =  GetWMIProperties("Win32_SystemProcesses").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemProgramGroups =  GetWMIProperties("Win32_SystemProgramGroups").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemResources =  GetWMIProperties("Win32_SystemResources").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemServices =  GetWMIProperties("Win32_SystemServices").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemSlot =  GetWMIProperties("Win32_SystemSlot").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemSystemDriver =  GetWMIProperties("Win32_SystemSystemDriver").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemTimeZone =  GetWMIProperties("Win32_SystemTimeZone").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var SystemUsers =  GetWMIProperties("Win32_SystemUsers").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var TapeDrive =  GetWMIProperties("Win32_TapeDrive").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var TCPIPPrinterPort =  GetWMIProperties("Win32_TCPIPPrinterPort").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var TemperatureProbe =  GetWMIProperties("Win32_TemperatureProbe").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var TerminalService =  GetWMIProperties("Win32_TerminalService").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Thread =  GetWMIProperties("Win32_Thread").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var TimeZone =  GetWMIProperties("Win32_TimeZone").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var TypeLibraryAction =  GetWMIProperties("Win32_TypeLibraryAction").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var USBController =  GetWMIProperties("Win32_USBController").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var USBControllerDevice =  GetWMIProperties("Win32_USBControllerDevice").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var USBHub =  GetWMIProperties("Win32_USBHub").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var UserAccount =  GetWMIProperties("Win32_UserAccount").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var UserDesktop =  GetWMIProperties("Win32_UserDesktop").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var UserInDomain =  GetWMIProperties("Win32_UserInDomain").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var UserProfile =  GetWMIProperties("Win32_UserProfile").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var UTCTime =  GetWMIProperties("Win32_UTCTime").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var VideoController =  GetWMIProperties("Win32_VideoController").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var VideoSettings =  GetWMIProperties("Win32_VideoSettings").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var VoltageProbe =  GetWMIProperties("Win32_VoltageProbe").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var Volume =  GetWMIProperties("Win32_Volume").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var VolumeQuota =  GetWMIProperties("Win32_VolumeQuota").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var VolumeQuotaSetting =  GetWMIProperties("Win32_VolumeQuotaSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var VolumeUserQuota =  GetWMIProperties("Win32_VolumeUserQuota").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var WMIElementSetting =  GetWMIProperties("Win32_WMIElementSetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );
//var WMISetting =  GetWMIProperties("Win32_WMISetting").Select(li => new{ li.Name, li.Value, li.Type }).Where(w => w.Value != null );

	


}

// Define other methods and classes here

public class DataItem
{
	public DataItem(){}
	public DataItem(string name, object value, Enum type)
	{
		name = Name;
		value = Value;
		type = Type;
	}
	public String Name {get;set;}
	public Object Value {get;set;}
	public Enum Type {get;set;}
}

private List<PropertyData> GetWMIProperties(String QueryDomain)
{
	try
	{	        
		using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(
		CreateNewManagementScope("DEN-sp-01",@"emllc\eswadmin","txkjlxa12"), 
		new SelectQuery(String.Format("SELECT * FROM {0}",QueryDomain))))
			{
				using(ManagementObjectCollection classes = searcher.Get())
					{
						return classes.Cast<ManagementBaseObject>().FirstOrDefault().Properties.Cast<PropertyData>().Where(w => w.Value != null ).ToList();
					}
			}	
	}
	catch (Exception ex)
	{
	
 		return new List<PropertyData>();
		//Escape out of any errors...because it's not there...
 		QueryDomain.Dump();
		//throw;
	}
	
 
	
}


private ManagementScope CreateNewManagementScope(string server, string userName, string password)
        {
            string serverString = @"\\" + server + @"\root\cimv2";

            ManagementScope scope = new ManagementScope(serverString);

                ConnectionOptions options = new ConnectionOptions
                                  {
                                      Username = userName,
                                      Password = password,
                                      Impersonation = ImpersonationLevel.Impersonate,
                                      Authentication = AuthenticationLevel.PacketPrivacy
                                  };
                scope.Options = options;

            return scope;
        }