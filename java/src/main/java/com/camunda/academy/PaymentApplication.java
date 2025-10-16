
package com.camunda.academy;

import java.net.URI;
import java.util.Scanner;

import com.camunda.academy.handler.YourJobHandler;

import io.camunda.client.CamundaClient;
import io.camunda.client.api.worker.JobWorker;
import io.camunda.client.impl.oauth.OAuthCredentialsProvider;
import io.camunda.client.impl.oauth.OAuthCredentialsProviderBuilder;

public class PaymentApplication {

    // Zeebe Client Credentials
    private static final String ZEEBE_ADDRESS = "XXXXXXXXXXXX.XXXX.zeebe.camunda.io";
    private static final String ZEEBE_CLIENT_ID = "XXXXXXXXXXXX";
    private static final String ZEEBE_CLIENT_SECRET = "XXXXXXXXXXXXXXXXXXXXXXXX";
    private static final String ZEEBE_AUTHORIZATION_SERVER_URL = "https://login.cloud.camunda.io/oauth/token";
    private static final String ZEEBE_TOKEN_AUDIENCE = "zeebe.camunda.io";

    public static void main(String[] args) {

        final OAuthCredentialsProvider credentialsProvider = new OAuthCredentialsProviderBuilder()
                .authorizationServerUrl(ZEEBE_AUTHORIZATION_SERVER_URL)
                .audience(ZEEBE_TOKEN_AUDIENCE)
                .clientId(ZEEBE_CLIENT_ID)
                .clientSecret(ZEEBE_CLIENT_SECRET)
                .build();

        try (final CamundaClient client = CamundaClient.newClientBuilder()
                .grpcAddress(URI.create("https://" + ZEEBE_ADDRESS))
                .credentialsProvider(credentialsProvider)
                .build()) {

            // Request the Cluster Topology
            System.out.println("Connected to: " + client.newTopologyRequest().send().join());

            // Start a Job Worker
            final JobWorker creditCardWorker = client.newWorker()
                    .jobType("YOUR_JOB_TYPE")
                    .handler(new YourJobHandler())
                    .open();

            // Terminate the worker with an Integer input
            Scanner sc = new Scanner(System.in);
            sc.nextInt();
            sc.close();
            creditCardWorker.close();

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
