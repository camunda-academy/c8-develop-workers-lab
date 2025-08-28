const config = require('../config.json');      /* API credentials */
const { Camunda8 } = require('@camunda8/sdk'); /* npm i @camunda8/sdk */

let client;

async function connect() {

    const c8 = new Camunda8(config);

    return c8;
}

(async () => {

    const c8 = await connect();

    client = c8.getCamundaRestClient();

    const topology = await client.getTopology();

    console.log(topology);

    client.createJobWorker({
        type: 'your-type-here',        /* Worker type? */
        timeout: 20000,
        maxJobsToActivate: 1,
        worker: 'some-name-worker',    /* Name your worker */
        jobHandler: jobHandlerFunction /* Name your handler function */
    })

    /* Add additional workers here */

})()

async function jobHandlerFunction(job) { /* Name your handler function */

    console.log("Worker is doing something...");

    /* Make sure you complete the job */
}

/***** These are your "services". We will use these later in Exercise 6 *****/

function getCustomerCredit(customerId) {

      let credit = 0.0;

      const regEx = /\d+/;

      const match = customerId.match(regEx);

      if (match) { credit = parseFloat(match); }

      return credit;
}

function deductCredit(amount, credit) {

      let openAmount = 0.0;

      if (credit < amount) { openAmount = amount - credit; }

      return openAmount;
}
