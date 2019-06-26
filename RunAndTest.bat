pushd IdentityServerHost
start dotnet run
popd
pushd SecureApiTests
dotnet test
popd
