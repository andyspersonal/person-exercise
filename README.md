# Coding exercise

## Initial Thoughts

- Currently the code will not compile and it would be easier to work with if it did so I will make some "stub" classes to allow it to compile.
- The register function has a complex flow as it is deeply nested.  This makes it hard to read and can lead to bugs being introduced
- Variables are passed to the Register function but not used, but properties of the class are.  This can be confusing.  It also means that properties in the class need to be set before calling this function, which might not be clear to a consuming developer.
- The class has many responsibilities which increases complexity, makes maintenance harder and means creating unit tests will be more complex.  It looks likely that this can be split into two classes, a service to handle what needs to happen during registration and an entity to contain properties and business logic.
- The naming convention for variables is inconsistent, for readability these should be consistant.  Some variables do not seem to be clearly named.

## Separate out Service and Entity into their own classes
Make a register service that takes the speaker as a parameter and has any services injected into it. This reduces the number of responsibilaities of the person class and makes it more of an Entity class rather than a mix of a service and an Entity.  The new service will have an interface so that it can be injected into any consumer.

## Reduce complexity in the register service
The level of nesting in the register function is due to validation of the speaker.  This can be simplified by doing all the validation first in the function and returning early.  Putting it in to its won function allows it to be called independently and makes testing easier.  The checks for null or zero length will also be changed to use the NullOrEmpty.  Might be even better to use NullorWhiteSpace.

When doing this I noticed that the list of domains and employees are hard coded and it can be seen from the comments that this has had to be changed in the past.  Typically this would be provided dynamically, say from a database, which could easily be achived by adding a new repository method.  The validation result could also provide a list of validation issues, which would be more usefull to the consumer.

During this process I have renamed some variables to make them easier to read.  Using full names rather than abbreviations makes the code easier to read.

The validation around experience, certification, employers, emails address and web browser seems odd.  I have left it functioning the same (but made it easier to read).

Validation could be moved to its own service and injected into the register service, it could make testing a little more focused.  But that feels a little excesive for the size of project currently.

## Move calculation of the registration fee to the speaker class
This puts the domain logic in the entity class rather than the register function (DDD).  It helps to simplify the register function and keep it to a single responsibility.  I have set the registration fee to be a decimal as this likely represents a monetary value.  Handled possible null value for Experience.  Also improved the comments for the public functions.

## Exception handing when saving to the database
Currently the exception when saving to the database is ignored.  The only way the consumer would know is that the returned speakerId would be null.  There are two options here.  A new RegisterError type could be added or the exception could be allowed to bubble up.  Given that the other errors are related to validation or business logic rather than some sort of infrastructure failure I have decided to let the exception bubble up.  Because of this I have changes the save speaker to return a non nullable int.



## Possible further enhancments
 - Seprate request object as browser probably should not be in the speaker class. 
- BrowserName to be addressed, proibably it own enum not in the WebBroowser class, or may be have a is modern browser function.
 - Could change HasBlog to be calculated from BlogUrl is null
 - get list of domains and employers from a repository
 - consider turning on nullable to protect from null exceptions
 - When doing string comparisons, consdier ignoring white space and capitalization (Extension method)
