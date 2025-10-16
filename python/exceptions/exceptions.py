# exceptions.py

from pyzeebe import Job, JobController

class InvalidExpiryDateException(Exception):
    """Raised when a credit card has an invalid expiry date."""
    pass


async def credit_card_charging_exception_handler(exception: Exception, job: Job, job_controller: JobController) -> None:
    """
    Handles exceptions related to credit card charging.
    """
    print(exception)
    if isinstance(exception, InvalidExpiryDateException):
        # Handle invalid expiry date
        pass
    else:
        # Handle other errors
        pass
    return
