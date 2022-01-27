# Run each time the container is successfully started.
# see https://code.visualstudio.com/docs/remote/devcontainerjson-reference#_lifecycle-scripts

# Start minikube
# nohup bash -c 'minikube start &' > minikube.log 2>&1

# Ensure minikube cluster exists, or create it.
# ctlptl apply -f ./devcluster.yaml