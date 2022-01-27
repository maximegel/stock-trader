"Go containers dependencies"

load("@io_bazel_rules_docker//go:image.bzl", "repositories")
load("@io_bazel_rules_docker//container:container.bzl", "container_pull")

def docker_go_dependencies():
    repositories()

    container_pull(
        name = "go_image_base",
        registry = "docker.io",
        repository = "library/golang",
        tag = "alpine",
    )
