import { createCamundaClient } from "@camunda8/orchestration-cluster-api";

const config = require('../config.json');   // API Credentials

const client = createCamundaClient({
    config: {
        CAMUNDA_REST_ADDRESS: config.CAMUNDA_REST_ADDRESS,
        CAMUNDA_AUTH_STRATEGY: "OAUTH",
        CAMUNDA_CLIENT_ID: config.CAMUNDA_CLIENT_ID,
        CAMUNDA_CLIENT_SECRET: config.CAMUNDA_CLIENT_SECRET,
        CAMUNDA_OAUTH_URL: config.CAMUNDA_OAUTH_URL,
        CAMUNDA_TOKEN_AUDIENCE: "zeebe.camunda.io",
    },
    log: { level: "debug" },
});

(async () => {

    const topology = await client.getTopology();

    console.log(topology);

    // Job Workers

    client.createJobWorker({
        jobType: 'job-type-here',
        jobTimeoutMs: 20000,
        maxParallelJobs: 1,
        workerName: 'worker-name-here',
        jobHandler: jobHandler
    })

    // ...

})()

// Job Handlers

async function jobHandler(job) {

    console.log("Handling job...");

    await job.complete();
}

// ...
