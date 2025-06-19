@echo off
echo TEST API

set TOKEN=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdHVzZXIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0QHRlc3QuY29tIiwiZXhwIjoxNzQ5NzU3MTQyfQ.uAuQO8ZdbStm_xhAeGmht_iz0v9JeWFINPdPpw5Ap9c
REM === REST API ===

REM 1️⃣ GET all users
echo == GET /api/User ==
curl -X GET https://localhost:7233/api/User -k
echo.

REM 2️⃣ POST create user
echo == POST /api/User ==
curl -X POST https://localhost:7233/api/User ^
    -H "Authorization: Bearer %TOKEN%" ^
    -H "Content-Type: application/json" ^
    -d "{\"name\":\"TestUser\",\"email\":\"testuser@test.com\"}" -k
echo.

REM 3️⃣ DELETE user with ID 1 (example)
echo == DELETE /api/User/1 ==
curl -X DELETE https://localhost:7233/api/User/1 ^
    -H "Authorization: Bearer %TOKEN%" -k
echo.

REM 4️⃣ GET /api/TaskItem
echo == GET /api/TaskItem ==
curl -X GET https://localhost:7233/api/TaskItem ^
    -H "Authorization: Bearer %TOKEN%" -k
echo.

REM 5️⃣ POST /api/TaskItem
echo == POST /api/TaskItem ==
curl -X POST https://localhost:7233/api/TaskItem ^
    -H "Authorization: Bearer %TOKEN%" ^
    -H "Content-Type: application/json" ^
    -d "{\"title\":\"Task from curl\",\"isDone\":false,\"userId\":1,\"projectId\":1}" -k
echo.

REM === GraphQL ===

REM 6️⃣ GraphQL Query users
echo == GraphQL Query users ==
curl -X POST https://localhost:7233/graphql ^
    -H "Authorization: Bearer %TOKEN%" ^
    -H "Content-Type: application/json" ^
    -d "{\"query\":\"{ users { id name email } }\"}" -k
echo.

REM 7️⃣ GraphQL Mutation createUser
echo == GraphQL Mutation createUser ==
curl -X POST https://localhost:7233/graphql ^
    -H "Authorization: Bearer %TOKEN%" ^
    -H "Content-Type: application/json" ^
    -d "{\"query\":\"mutation { createUser(user: { name: \\\"UserFromCurl\\\", email: \\\"usercurl@test.com\\\" }) { id name email } }\"}" -k
echo.

echo == POST /api/Auth/login ==
curl -X POST https://localhost:7233/api/Auth/login ^
    -H "Content-Type: application/json" ^
    -d "{\"name\":\"testuser\",\"email\":\"test@test.com\"}" -k
pause


pause
