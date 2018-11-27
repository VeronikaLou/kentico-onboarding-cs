�
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Helpers\IGuidGenerator.cs
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
}		 �
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Helpers\ITimeManager.cs
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
}		 �
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Helpers\IUrlLocator.cs
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
}		 �
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\IBootstrapper.cs
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
}		 �
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Models\Item.cs
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
}	 �
.
� �
"
� �
;
� �
} 
} �
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Models\RetrievedItem.cs
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
 �
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Properties\AssemblyInfo.cs
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
]##) *�
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Repositories\IConnectionString.cs
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
} �	
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Repositories\IListRepository.cs
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
} �
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Services\ICreateItemService.cs
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
 �
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Services\IGetItemService.cs
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
} �
�C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\src\KenticoOnboardingApplication.Contracts\Services\IUpdateItemService.cs
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