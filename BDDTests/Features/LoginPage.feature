Feature: LoginPage
	To be able to create new task
	as a user I need to log in

@NotSignedIn
Scenario: Login with empty values
	Given I am on Login Page
	When I click Submit
	Then I see validation message on the top
	Then I see password validation
	Then I see login validation

@NotSignedIn
Scenario: Login with invalid data
	Given I navigate and login with random login and random password
	* I wait for LoginFailure page to be loaded
	Then I see header validation
	Then I see invalid data warning message

@SignedIn
Scenario Outline: Login with valid data
	Given I navigate and login with <correctLogin> login and <correctPassword> password
	* I wait for Task page to be loaded
	Then I am redirected to Tasks Page
	Examples: 
	| correctLogin | correctPassword |
	| jane         | password        |
	| JANE         | PASSWORD        |
	| Jane         | Password        |
