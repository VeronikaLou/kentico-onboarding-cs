Ù
íC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Helpers\IGuidGenerator.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
	Contracts' 0
.0 1
Helpers1 8
{ 
public 

	interface 
IGuidGenerator #
{ 
Guid 

GenerateId 
( 
) 
; 
} 
}		 ¯
êC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Helpers\ITimeManager.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
	Contracts' 0
.0 1
Helpers1 8
{ 
public 

	interface 
ITimeManager !
{ 
DateTime 
GetDateTimeNow 
(  
)  !
;! "
} 
}		 ì
èC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Helpers\IUrlLocator.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
	Contracts' 0
.0 1
Helpers1 8
{ 
public 

	interface 
IUrlLocator  
{ 
Uri 
GetListItemUri 
( 
Guid 
id  "
)" #
;# $
} 
}		 É
âC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\IBootstrapper.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
	Contracts' 0
{ 
public 

	interface 
IBootstrapper "
{ 
IUnityContainer 
Register  
(  !
IUnityContainer! 0
	container1 :
): ;
;; <
} 
}		 Ï
áC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Models\Item.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
	Contracts' 0
.0 1
Models1 7
{ 
public 

class 
Item 
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
Text 
{ 
get  
;  !
set" %
;% &
}' (
public		 
DateTime		 
LastUpdateTime		 &
{		' (
get		) ,
;		, -
set		. 1
;		1 2
}		3 4
public

 
DateTime

 
CreationTime

 $
{

% &
get

' *
;

* +
set

, /
;

/ 0
}

1 2
public 
override 
string 
ToString '
(' (
)( )
=>* ,
$"- /
Item / 4
{4 5
Id5 7
}7 8
 = 8 ;
{; <
Text< @
}@ A
, was created at A R
{R S
CreationTimeS _
}_ `
 and updated at ` p
{p q
LastUpdateTimeq 
}	 Ä
.
Ä Å
"
Å Ç
;
Ç É
} 
} ˝
êC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Models\RetrievedItem.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
	Contracts' 0
.0 1
Models1 7
{ 
public 

class 
RetrievedItem 
{ 
public 
Item 
Item 
{ 
get 
; 
set  #
;# $
}% &
public 
bool 
WasFound 
=> 
Item  $
!=% '
null( ,
;, -
public 
RetrievedItem 
( 
Item !
item" &
)& '
=>( *
Item+ /
=0 1
item2 6
;6 7
}		 
}

 ø
ìC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str A
)A B
]B C
[ 
assembly 	
:	 

AssemblyDescription 
( 
$str !
)! "
]" #
[		 
assembly		 	
:			 
!
AssemblyConfiguration		  
(		  !
$str		! #
)		# $
]		$ %
[

 
assembly

 	
:

	 

AssemblyCompany

 
(

 
$str

 
)

 
]

 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str C
)C D
]D E
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
["" 
assembly"" 	
:""	 

AssemblyVersion"" 
("" 
$str"" $
)""$ %
]""% &
[## 
assembly## 	
:##	 

AssemblyFileVersion## 
(## 
$str## (
)##( )
]##) *ï
öC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Repositories\IConnectionString.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
	Contracts' 0
.0 1
Repositories1 =
{ 
public 

	interface 
IConnectionString &
{ 
string 
TodoList 
{ 
get 
; 
}  
} 
} Û	
òC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Repositories\IListRepository.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
	Contracts' 0
.0 1
Repositories1 =
{ 
public 

	interface 
IListRepository $
{		 
Task

 
<

 
IEnumerable

 
<

 
Item

 
>

 
>

 
GetAllItemsAsync

  0
(

0 1
)

1 2
;

2 3
Task 
< 
Item 
> 
GetItemAsync 
(  
Guid  $
id% '
)' (
;( )
Task 
< 
Item 
> 
AddItemAsync 
(  
Item  $
item% )
)) *
;* +
Task 
< 
Item 
> 
UpdateItemAsync "
(" #
Item# '
item( ,
), -
;- .
Task 
DeleteItemAsync 
( 
Guid !
id" $
)$ %
;% &
} 
} ◊
óC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Services\ICreateItemService.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
	Contracts' 0
.0 1
Services1 9
{ 
public 

	interface 
ICreateItemService '
{ 
Task 
< 
Item 
> 
CreateItemAsync "
(" #
Item# '
item( ,
), -
;- .
}		 
}

 ’
îC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Services\IGetItemService.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
	Contracts' 0
.0 1
Services1 9
{ 
public 

	interface 
IGetItemService $
{ 
Task		 
<		 
RetrievedItem		 
>		 
GetItemAsync		 (
(		( )
Guid		) -
id		. 0
)		0 1
;		1 2
}

 
} ‡
óC:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Services\IUpdateItemService.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
	Contracts' 0
.0 1
Services1 9
{ 
public 

	interface 
IUpdateItemService '
{ 
Task 
< 
RetrievedItem 
> 
UpdateItemAsync +
(+ ,
Item, 0
item1 5
)5 6
;6 7
}		 
}

 