
IACS interface
=====

### 1. Generate a token for authentication

POST https://learnumis2020.government.bg/api/token

#### Request header

 | KEY              | VALUE                                     |
 | ---------------- |:-----------------------------------------:|
 | content-type     | application/x-www-form-urlencoded         |

#### Request body

 | KEY              | VALUE                                     |
 | ---------------- |:-----------------------------------------:|
 | client_id        | iacs_client                               |
 | client_secret    | *<client_secret>*                         |
 | grant_type       | client_credentials                        |
 | scope            | external:iacs_service                     |
 
#### Result

```
{
    "access_token": <client_token>,
    "token_type": "bearer",
    "expires_in": 1199
}
```

The returned `access_token` is used for authentication in the next request.

##

### 2. Send contract data

POST https://learnumis2020.government.bg/api/historicContracts

#### Request header

 | KEY              | VALUE                                     |
 | ---------------- |:-----------------------------------------:|
 | Authorization    | Bearer <client_token>                     |
 | content-type     | application/json                          |
 
#### Result

- On success:
  - status code: **200**
- On error:
  - status code: **400**
  - body:
```
{
    "errorCode": <errorCode>,
    "errorMessage": <errorMessage>
}
```

Possible values of `errorCode`:
  - INVALID_PROCEDURE
  - INVALID_COMPANY_UIN_TYPE
  - INVALID_COMPANY_TYPE_CODE
  - INVALID_EXECUTION_STATUS
  - INVALID_NUTS_LEVEL
  - INVALID_LOCATION

Example error body:

```
{
    "errorCode": "INVALID_PROCEDURE",
    "errorMessage": "Not found procedure with code <some_code> in contract <number_of_contract_in_json> 
                     (with registration number <reg_number>)!"
}
```