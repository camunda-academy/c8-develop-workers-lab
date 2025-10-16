# workers.py

import asyncio
import configparser
from pyzeebe import ZeebeClient, ZeebeWorker, ZeebeTaskRouter, create_camunda_cloud_channel, Job, JobController

from services.credit_service import get_customer_credit, deduct_credit
from services.credit_card_service import charge_credit_card
from exceptions.exceptions import credit_card_charging_exception_handler


# Define Zeebe tasks
router = ZeebeTaskRouter()

@router.task("example-job-type")
def example_job_handler(job: Job):
    print("Hello world")
    return


async def main():
    
    # Get Config
    config = configparser.ConfigParser()
    config.read("config.ini")

    # Set up client and worker
    grpc_channel = create_camunda_cloud_channel(
        client_id=config["camunda"]["camunda.client.auth.client-id"],
        client_secret=config["camunda"]["camunda.client.auth.client-secret"],
        cluster_id=config["camunda"]["camunda.client.cloud.cluster-id"],
        region=config["camunda"]["camunda.client.cloud.region"]
    )
    global zeebe_client 
    zeebe_client = ZeebeClient(grpc_channel)
    worker = ZeebeWorker(grpc_channel)
    worker.include_router(router)
    print("Workers starting...")
    await worker.work()


if __name__ == "__main__":
    asyncio.run(main())