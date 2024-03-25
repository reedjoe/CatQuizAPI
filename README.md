# CatQuiz

## Assumptions made

- I have assumed that the list of Breeds returned by the external API is static enough that it can be safely cached on startup of the application
- I have assumed that calling the `images/search` endpoint with a single value for the `breed_ids` param only returns results with a single breed
  - I can't find anything in the external API's documentation to confirm how this filtering logic has been implemented, but it was always the case during my local testing
  - If this assumption is wrong, I would need to implement a retry policy when retrieving an image to check that the result is a single breed, and retry an arbitrary number of times if needed
  - In a real world scenario I would likely reach out to the developers of the external API for confirmation

## Improvements

- Add a check to prevent users from being shown the same picture for multiple questions
- Add some degree of 'weighting' to the rankings that accounts for the number of questions answered
  - Currently a user could answer a single question correctly and rank 1st due to having a 100% success rate
- Avoid having to calculate the leaderboard data for every request
  - While this is fine for the basic example with just a few users, it wouldn't scale well for a bigger application
  - Potentially this data could be cached/denormalised to improve performance
- Add pagination to the rankings endpoint

## Possible new features

- An endpoint that allows for retrieving any unanswered questions. With the current implementation, if the client loses the id of a question (e.g. user accidentally closes a tab) then there is no way to access it again
