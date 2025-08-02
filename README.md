# Coding exercise

## Intial Thoughts

- Currently the code will not not compile and it would be easier to work with if it did so I will make some "stub" classes to allow it to compile.
- The register function has a complex flow as it is deeply nested.  This makes is hard to read and can lead to bugs being introduced
- Varables are passed to the REgister function but not used, but properties of the class are.  This can be confusing.  It also means that properties in the class need to be set before calling this function, which mioght not be clear to a consuming developer.
- The class has many responsibilkities which increses complexity, makes maintinance harder and mean creating unit tests will be more complex.  This looks likely that this can be split into two classes, as service and an entity.
- The naming convention for variables is inconsistant, for readability these should be consistant.  Some variable do not seem to be well named.


## Seperate out Service and entity into their own classes
Make a register service that takes the speaker as a parameter and has any services injected into it. This reduces the number of responsibilities of the person class and makes it more of an Entity class rather than a mix of a service and an Entity.

## reduce complexity if the register service
The level of nesting in the register function is due to validation of the speaker.  This can be simplfied by doing all the validation first in the function and returning early.  Further more all the validation can be put into the Speaker class, associating the domain logic with the class (DDD).  The checks for null or zero length will also be change to use the NullOrEmpty string extension.  Might be even better to use NullorWhiteSpace, but this does chnage the functionality.

When doping this I noticed that the list of domains and employees are hard coded.  Typically this would be provided dynamically, say from a database.  It is worth noting that if this were the case then this part of the validation would have to be moved out of the Person class as the validation would require a database call.  The validation could also provide a list of validation issues, which would be more usefull to the consumer.

During this process I have renamed some variables to make them easier to read.  Using full names rather than abbreviations makes the code easier to read.

The validation around experince, certification, employers emails address and web browser seems odd.  I have left it functioning the same, but I think this logic would warrant questioning.







