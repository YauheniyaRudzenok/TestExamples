Feature: TaskPageDefaultTestFeature
	As not logged in user I need to see the site
	And be able to navigate through it

@NotSignedIn
Scenario: Navigate to login page
	Given user is on Home page
	When he clicks Log In page
	Then Log in page is opened

Scenario: Navigate to About page
	Given user is on Home page
	When he clicks About page
	Then About page is opened