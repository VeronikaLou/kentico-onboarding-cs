‘
C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Services\Helpers\GuidGenerator.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Services' /
./ 0
Helpers0 7
{ 
internal 
class 
GuidGenerator  
:  !
IGuidGenerator" 0
{ 
public 
Guid 

GenerateId 
( 
)  
=>! #
Guid$ (
.( )
NewGuid) 0
(0 1
)1 2
;2 3
}		 
}

 ø
ŽC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Services\Helpers\TimeManager.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Services' /
./ 0
Helpers0 7
{ 
internal 
class 
TimeManager 
: 
ITimeManager  ,
{ 
public 
DateTime 
GetDateTimeNow &
(& '
)' (
=>) +
DateTime, 4
.4 5
UtcNow5 ;
;; <
}		 
}

 Ï
’C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Services\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str @
)@ A
]A B
[		 
assembly		 	
:			 

AssemblyDescription		 
(		 
$str		 !
)		! "
]		" #
[

 
assembly

 	
:

	 
!
AssemblyConfiguration

  
(

  !
$str

! #
)

# $
]

$ %
[ 
assembly 	
:	 

AssemblyCompany 
( 
$str 
) 
] 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str B
)B C
]C D
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
[## 
assembly## 	
:##	 

AssemblyVersion## 
(## 
$str## $
)##$ %
]##% &
[$$ 
assembly$$ 	
:$$	 

AssemblyFileVersion$$ 
($$ 
$str$$ (
)$$( )
]$$) *
[%% 
assembly%% 	
:%%	 

InternalsVisibleTo%% 
(%% 
$str%% K
)%%K L
]%%L M
C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Services\ServicesBootstrapper.cs
	namespace		 	(
KenticoOnboardingApplication		
 &
.		& '
Services		' /
{

 
public 

class  
ServicesBootstrapper %
:% &
IBootstrapper' 4
{ 
public 
IUnityContainer 
Register '
(' (
IUnityContainer( 7
	container8 A
)A B
=>C E
	container 
. 
RegisterType 
< 
ICreateItemService 0
,0 1
CreateItemService2 C
>C D
(D E
newE H'
HierarchicalLifetimeManagerI d
(d e
)e f
)f g
. 
RegisterType 
< 
IUpdateItemService 0
,0 1
UpdateItemService2 C
>C D
(D E
newE H'
HierarchicalLifetimeManagerI d
(d e
)e f
)f g
. 
RegisterType 
< 
IGetItemService -
,- .
GetItemService/ =
>= >
(> ?
new? B'
HierarchicalLifetimeManagerC ^
(^ _
)_ `
)` a
. 
RegisterType 
< 
IGuidGenerator ,
,, -
GuidGenerator. ;
>; <
(< =
new= @'
HierarchicalLifetimeManagerA \
(\ ]
)] ^
)^ _
. 
RegisterType 
< 
ITimeManager *
,* +
TimeManager, 7
>7 8
(8 9
new9 <'
HierarchicalLifetimeManager= X
(X Y
)Y Z
)Z [
;[ \
} 
} Ø

’C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Services\Services\GetItemService.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Services' /
./ 0
Services0 8
{ 
internal		 
class		 
GetItemService		 !
:		" #
IGetItemService		$ 3
{

 
private 
readonly 
IListRepository (
_repository) 4
;4 5
public 
GetItemService 
( 
IListRepository -
respository. 9
)9 :
=>; =
_repository> I
=J K
respositoryL W
;W X
public 
async 
Task 
< 
RetrievedItem '
>' (
GetItemAsync) 5
(5 6
Guid6 :
id; =
)= >
{ 	
var 
databaseItem 
= 
await $
_repository% 0
.0 1
GetItemAsync1 =
(= >
id> @
)@ A
;A B
return 
new 
RetrievedItem $
($ %
databaseItem% 1
)1 2
;2 3
} 	
} 
} ¬
•C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Services\Services\UpdateItemService.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Services' /
./ 0
Services0 8
{ 
internal		 
class		 
UpdateItemService		 $
:		% &
IUpdateItemService		' 9
{

 
private 
readonly 
IListRepository (
_repository) 4
;4 5
private 
readonly 
ITimeManager %
_timeManager& 2
;2 3
private 
readonly 
IGetItemService (
_getItemService) 8
;8 9
public 
UpdateItemService  
(  !
IListRepository! 0

repository1 ;
,; <
ITimeManager= I
timeManagerJ U
,U V
IGetItemServiceW f
getItemServiceg u
)u v
{ 	
_repository 
= 

repository $
;$ %
_timeManager 
= 
timeManager &
;& '
_getItemService 
= 
getItemService ,
;, -
} 	
public 
async 
Task 
< 
RetrievedItem '
>' (
UpdateItemAsync) 8
(8 9
Item9 =
item> B
)B C
{ 	
var 
retrievedItem 
= 
await  %
_getItemService& 5
.5 6
GetItemAsync6 B
(B C
itemC G
.G H
IdH J
)J K
;K L
if 
( 
! 
retrievedItem 
. 
WasFound '
)' (
return 
retrievedItem $
;$ %
var 
updatedItem 
= 
new !
Item" &
{ 
Id 
= 
item 
. 
Id 
, 
Text 
= 
item 
. 
Text  
,  !
CreationTime   
=   
retrievedItem   ,
.  , -
Item  - 1
.  1 2
CreationTime  2 >
,  > ?
LastUpdateTime!! 
=!!  
_timeManager!!! -
.!!- .
GetDateTimeNow!!. <
(!!< =
)!!= >
}"" 
;"" 
var## 
result## 
=## 
await## 
_repository## *
.##* +
UpdateItemAsync##+ :
(##: ;
updatedItem##; F
)##F G
;##G H
return%% 
new%% 
RetrievedItem%% $
(%%$ %
result%%% +
)%%+ ,
;%%, -
}&& 	
}'' 
}(( ‰
•C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Services\Services\CreateItemService.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Services' /
./ 0
Services0 8
{ 
internal		 
class		 
CreateItemService		 $
:		% &
ICreateItemService		' 9
{

 
private 
readonly 
IListRepository (
_repository) 4
;4 5
private 
readonly 
IGuidGenerator '
_guidGenerator( 6
;6 7
private 
readonly 
ITimeManager %
_timeManager& 2
;2 3
public 
CreateItemService  
(  !
IListRepository! 0

repository1 ;
,; <
IGuidGenerator= K
guidGeneratorL Y
,Y Z
ITimeManager[ g
timeManagerh s
)s t
{ 	
_repository 
= 

repository $
;$ %
_guidGenerator 
= 
guidGenerator *
;* +
_timeManager 
= 
timeManager &
;& '
} 	
public 
async 
Task 
< 
Item 
> 
CreateItemAsync  /
(/ 0
Item0 4
item5 9
)9 :
{ 	
var 
dateTimeNow 
= 
_timeManager *
.* +
GetDateTimeNow+ 9
(9 :
): ;
;; <
var 
newItem 
= 
new 
Item "
{ 
Text 
= 
item 
. 
Text  
,  !
Id 
= 
_guidGenerator #
.# $

GenerateId$ .
(. /
)/ 0
,0 1
CreationTime 
= 
dateTimeNow *
,* +
LastUpdateTime 
=  
dateTimeNow! ,
} 
; 
return!! 
await!! 
_repository!! $
.!!$ %
AddItemAsync!!% 1
(!!1 2
newItem!!2 9
)!!9 :
;!!: ;
}"" 	
}## 
}$$ 