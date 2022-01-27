### Go ###
# See https://github.com/bazelbuild/rules_go

load("//tools/bazel/go:install.bzl", "go_rules_install")

go_rules_install()

load("//tools/bazel/go:deps.bzl", "go_dependencies")

go_dependencies()

### Docker ###
# See https://github.com/bazelbuild/rules_docker

load("//tools/bazel/docker:install.bzl", "docker_rules_install")

docker_rules_install()

load("//tools/bazel/docker:deps.bzl", "docker_dependencies")
load("//tools/bazel/docker/go:deps.bzl", "docker_go_dependencies")

docker_dependencies()

docker_go_dependencies()

### Kubernetes ###
# See https://github.com/bazelbuild/rules_k8s

load("//tools/bazel/k8s:install.bzl", "k8s_rules_install")

k8s_rules_install()

load("@io_bazel_rules_k8s//k8s:k8s.bzl", "k8s_defaults")
load("//tools/bazel/k8s:deps.bzl", "k8s_dependencies")

k8s_dependencies()

k8s_defaults(
    name = "k8s_deploy",
    cluster = "minikube",
    kind = "deployment",
)
