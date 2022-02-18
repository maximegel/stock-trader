"Tilt configuration, see https://docs.tilt.dev/api.html"

load("./apps/brokerage-api/Tiltfile", "brokerage_api")
load("./apps/portfolios-api/Tiltfile", "portfolios_api")

brokerage_api(host_port = 8000)
portfolios_api(host_port = 8100)
