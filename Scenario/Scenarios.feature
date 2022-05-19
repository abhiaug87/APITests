Feature: Scenarios for Testing API

@AC1
Scenario Outline: Display Category Name of the API
	When I pass headers for <content> and <status>
	Then I am able to see the category name with headers <content> and <status>
Examples:
| content          | status|
| application/json | false |

@AC2
Scenario Outline: Display CanRelist in API Body
	When I pass headers for <content> and <status>
	Then I am able to see the canrelist status with headers <content> and <status>
Examples:
| content          | status|
| application/json | false |

@AC3
Scenario Outline: Display Promotions Name in the API Body
	When I pass headers for <content> and <status>
	Then I am able to see the promotions name with headers <content> and <status>
Examples:
| content          | status|
| application/json | false |
