# Errors

## Not Found Error

- **UserNotFound** - Could not find the user
- **UserNotFoundWithToken** - Could not find the user with the Token Provided
- **UserNotFoundWithEmail** - Could not find the user with the email of {email}
- **PostNotFound** - Could not find the post with Id of {id}
- **CommentNotFound** - Could not find the comment with the Id of {id}
- **FriendNotFound** - Could not find the friend with Id of {id}
- **TagNotFound** - The post with the Id of {id} does not have the tag of {tag}

## No DBSet Found Error

- **NoCommentsFound** - There are no comments to mutate or query
- **NoPostsFound** - There are no posts to mutate or query
- **NoFriendshipsFound** - There are no friendships to mutate or query

## Creating Error

- **CreatePostError** - Could not create the Post
- **CreateCommentError** - Could not create the Comment
- **AddFriendError** - Could not add the Friend

## Deleting Error

- **DeleteCommentError** - Could not delete the comment with the Id of {id}
- **RemoveFriendError** - Could not remove the friend with the Id of {id}
- **DeletePostError** Could not delete the post with the Id of {id}

## Edit Error

- **EditCommentError** - Could not edit the Comment with the Id of {id}
- **EditPostError** - Could not edit the Post with the Id of {id}

## Validation Error

- **InvalidPassword** - The password provided is not the same as the hashed password

## Resource Exists Error

- **UserAlreadyExists** - User already exists in the database
- **PostAlreadyHasTag** - The post with the Id of {id} already has the tag of {tag}
