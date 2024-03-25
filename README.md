# CatQuiz

I chose to implement a vertical slice pattern for this solution, making use of the Mediatr library. Endpoints can be found in the `Features` directory. For each endpoint there is a directory containing two files:

- One containing the controller, plus the request and response objects
- Another containing the 'handler' with all of the business logic for the endpoint

I found this to work well in terms of readability for this simple solution, but more complicated endpoints may benefit from further code splitting.

To run this solution, please replace the text `ReplaceWithApiKey` in the relevant appsettings file with a valid API key.

## Assumptions made

- I have assumed that the list of Breeds returned by the external API is static enough that it can be safely cached on startup of the application.
- In order to avoid showing images with multiple breeds, I have assumed that calling the `images/search` endpoint with a single value for the `breed_ids` param only returns results with a single breed.
  - I couldn't find anything in the external API's documentation to confirm how this filtering logic has been implemented, but it was always the case during my local testing.
  - If this assumption is wrong, I would need to implement a retry policy when retrieving an image to check if the result has multiple breeds, and retry an arbitrary number of times if needed.
  - In a real world scenario I would likely reach out to the developers of the external API for confirmation.

## Improvements

- Add a check to prevent users from being shown the same picture for multiple questions.
- Add some degree of 'weighting' to the rankings that accounts for the number of questions answered.
  - Currently a user could answer a single question correctly and rank 1st due to having a 100% success rate.
- Avoid having to calculate the leaderboard data for every request.
  - While this is fine for the basic example with just a few users, it wouldn't scale well for a bigger application.
  - Potentially this data could be cached/denormalised to improve performance.
- Add pagination to the rankings endpoint.

## Possible new features

- An endpoint that allows for retrieving any unanswered questions. With the current implementation, if the client loses the id of a question (e.g. user accidentally closes a tab) then there is no way to access it again.
