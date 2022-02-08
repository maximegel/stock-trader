# Run each time the container is successfully started.
# see https://code.visualstudio.com/docs/remote/devcontainerjson-reference#_lifecycle-scripts

# Recreate and start minikube cluster.
minikube delete
docker stop minikube-registry
docker rm minikube-registry
ctlptl apply -f ./devcluster.yaml

# Prebuild .NET tooling targets.
bazel build @core_sdk_${DOTNET_TOOLCHAIN}//...
# Prebuild Go tooling targets.
bazel build @go_sdk//:go_sdk
bazel build :gopath
