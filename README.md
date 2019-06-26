# TestSecureApiSample

[![Build Status](https://dev.azure.com/ardalis/TestSecureApiSample/_apis/build/status/ardalis.TestSecureApiSample?branchName=master)](https://dev.azure.com/ardalis/TestSecureApiSample/_build/latest?definitionId=1&branchName=master)

A sample showing how to test a secure API endpoint using xunit, identityserver4, and environment variables.

## Goals

- Demonstrate how to test a token-secured API endpoint running live (perhaps in a container) using xUnit
- Demonstrate how to dynamically specify the API endpoint's URL using environment variables read by xUnit

## Original Sample

Original **IdentityServerHost** project forked from here:
[https://github.com/brockallen/IdentityServerAndApi](https://github.com/brockallen/IdentityServerAndApi) and only modified slightly.

## Expected Output using Environment Variables for Configuration

```console
Microsoft Windows [Version 10.0.17763.557]
(c) 2018 Microsoft Corporation. All rights reserved.

C:\dev\Scratch\IdentityServerAndApi\SecureAPITests>SET ApiBaseUrl=http://google.com

C:\dev\Scratch\IdentityServerAndApi\SecureAPITests>dotnet test
Test run for C:\dev\Scratch\IdentityServerAndApi\SecureAPITests\bin\Debug\netcoreapp3.0\SecureAPITests.dll(.NETCoreApp,Version=v3.0)
Microsoft (R) Test Execution Command Line Tool Version 16.0.1
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
[xUnit.net 00:00:00.61]     SecureAPITests.UnitTest1.HitApiEndpoint [FAIL]
Failed   SecureAPITests.UnitTest1.HitApiEndpoint
Error Message:
 Assert.True() Failure
Expected: True
Actual:   False
Stack Trace:
   at SecureAPITests.UnitTest1.HitApiEndpoint() in C:\dev\Scratch\IdentityServerAndApi\SecureAPITests\UnitTest1.cs:line 92
--- End of stack trace from previous location where exception was thrown ---

Total tests: 2. Passed: 1. Failed: 1. Skipped: 0.
Test Run Failed.
Test execution time: 1.0766 Seconds

C:\dev\Scratch\IdentityServerAndApi\SecureAPITests>SET ApiBaseUrl=http://localhost:5000

C:\dev\Scratch\IdentityServerAndApi\SecureAPITests>dotnet test
Test run for C:\dev\Scratch\IdentityServerAndApi\SecureAPITests\bin\Debug\netcoreapp3.0\SecureAPITests.dll(.NETCoreApp,Version=v3.0)
Microsoft (R) Test Execution Command Line Tool Version 16.0.1
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

Total tests: 2. Passed: 2. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 0.9730 Seconds

C:\dev\Scratch\IdentityServerAndApi\SecureAPITests>
```
