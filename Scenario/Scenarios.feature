Feature: Scenarios for Testing API

@AC1
Scenario Outline: Display Category Name of the API
	Given I have access to the URL for categories
	When I pass headers <content>, <corr>, <reqid>, <token>, <encoding> and <conn>
	Then I am able to see the category name with headers <content>
Examples:
| content          | corr                                 | reqid                                | token                                | encoding | conn       |
| application/json | 6eca3cbd-479c-4304-b272-619300dc99a2 | f1d45b16-405f-4060-add4-8a1256b2f765 | 93cd9499-8404-4a26-8dc9-7bbd7c91877c | gzip     | Keep-Alive |

@AC2
Scenario Outline: Display CanRelist in API Body
	Given I have access to the URL for categories
	When I pass headers <content>, <corr>, <reqid>, <token>, <encoding> and <conn>
	Then I am able to see the canrelist status with headers <content>
Examples:
| content          | corr                                 | reqid                                | token                                | encoding | conn       |
| application/json | 6eca3cbd-479c-4304-b272-619300dc99a2 | f1d45b16-405f-4060-add4-8a1256b2f765 | 93cd9499-8404-4a26-8dc9-7bbd7c91877c | gzip     | Keep-Alive |

@AC3
Scenario Outline: Display Promotions Name in the API Body
	Given I have access to the URL for categories
	When I pass headers <content>, <corr>, <reqid>, <token>, <encoding> and <conn>
	Then I am able to see the promotions name with headers <content>
Examples:
| content          | corr                                 | reqid                                | token                                | encoding | conn       |
| application/json | 6eca3cbd-479c-4304-b272-619300dc99a2 | f1d45b16-405f-4060-add4-8a1256b2f765 | 93cd9499-8404-4a26-8dc9-7bbd7c91877c | gzip     | Keep-Alive |
