# Notes:

- Mapped JSX can use RefetchContext

# Props:

## searchEnabled:

- bool value
- Shows search bar
- Calls _fetchItems_ with _search_ as a GET query parameter

## fetchItems:

- Service method for GET endpoint call to API with _page_ as a query parameter

## renderItem:

- Function which maps item from API response to JSX

## noItemsMessages:

- Array of texts to show when no items are retrieved

## onAddNew:

- If provided, a _+_ button is shown on the screen
- Prop itself is a function which is called on _+_ button click

## emptySearch:

- bool value
- If provided, search bar can be empty
- Otherwise, some value has to be entered to call the API
