Feature: TaskPageDefaultTestFeature
	As not logged in user I need to see the site
	And be able to navigate through it

@NotSignedIn
Scenario: Navigate to login page
	Given I am on Home page
	When I click Log In link
	Then I see log in page is opened

Scenario: Navigate to About page
	Given I am on Home page
	When I click About link
	Then I see About page is opened