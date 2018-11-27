ó
¡C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\test\KenticoOnboardingApplication.Tests.Base\Comparers\EqualConstraintExtensions.cs
	namespace 	(
KenticoOnboardingApplication
 &
.& '
Tests' ,
., -
Base- 1
.1 2
	Comparers2 ;
{ 
[		 
SuppressMessage		 
(		 
$str		  
,		  !
$str		" @
)		@ A
]		A B
public

 

static

 
class

 
ComparerWraper

 &
{ 
private 
static 
Lazy 
< 
ItemComparer (
>( )
LazyComparer* 6
=>7 9
new: =
Lazy> B
<B C
ItemComparerC O
>O P
(P Q
)Q R
;R S
private 
sealed 
class 
ItemComparer )
:* +
IEqualityComparer, =
<= >
Item> B
>B C
{ 	
public 
bool 
Equals 
( 
Item #
first$ )
,) *
Item+ /
second0 6
)6 7
{ 
if 
( 
first 
== 
null !
&&" $
second% +
==, .
null/ 3
)3 4
{ 
return 
true 
;  
} 
if 
( 
first 
== 
null !
||" $
second% +
==, .
null/ 3
)3 4
{ 
return 
false  
;  !
} 
return 
AreItemsEqual $
($ %
first% *
,* +
second, 2
)2 3
;3 4
} 
public 
int 
GetHashCode "
(" #
Item# '
item( ,
), -
=>. 0
item1 5
.5 6
Id6 8
.8 9
GetHashCode9 D
(D E
)E F
+G H
itemI M
.M N
TextN R
.R S
GetHashCodeS ^
(^ _
)_ `
;` a
}   	
public"" 
static"" 
EqualConstraint"" %
UsingItemComparer""& 7
(""7 8
this""8 <
EqualConstraint""= L

constraint""M W
)""W X
=>""Y [

constraint## 
.## 
Using## 
(## 
LazyComparer## )
.##) *
Value##* /
)##/ 0
;##0 1
public%% 
static%% 
bool%% 
AreItemsEqual%% (
(%%( )
Item%%) -
first%%. 3
,%%3 4
Item%%5 9
second%%: @
)%%@ A
=>%%B D
first&& 
.&& 
Id&& 
==&& 
second&& 
.&& 
Id&& !
&&&&" $
first'' 
.'' 
Text'' 
=='' 
second''  
.''  !
Text''! %
&&''& (
first(( 
.(( 
CreationTime(( 
==(( !
second((" (
.((( )
CreationTime(() 5
&&((6 8
first)) 
.)) 
LastUpdateTime))  
==))! #
second))$ *
.))* +
LastUpdateTime))+ 9
;))9 :
}** 
}++ Á
•C:\Users\VeronikaL\Desktop\kentico-onboarding-cs\KenticoOnboardingApplication\test\KenticoOnboardingApplication.Tests.Base\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str B
)B C
]C D
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
$str D
)D E
]E F
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
]$$) *