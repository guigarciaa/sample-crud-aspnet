import faker from "https://cdnjs.cloudflare.com/ajax/libs/Faker/3.1.0/faker.min.js";
import http from "k6/http";
import { sleep } from "k6";

export const options = {
  thresholds: {
    http_req_failed: ["rate<0.01"], // http errors should be less than 1%
    http_req_duration: ["p(95)<200"], // 95% of requests should be below 200ms
  },
  scenarios: {
    pre_warmup: {
      executor: "shared-iterations",
      vus: 2,
      iterations: 10,
      startTime: "0s",
    },
    warmup: {
      executor: "shared-iterations",
      vus: 5,
      iterations: 10,
      startTime: "10s",
    },
    stress: {
      executor: "ramping-vus",
      startTime: "20s",
      startVUs: 6,
      stages: [{ duration: "3m", target: 600 }],
    },
  },
};

export default (data) => {
  const url = "http://nginx:9999/api/person";
  const params = {
    headers: {
      "Content-Type": "application/json",
    },
  };
  // const data = Generator();
  const Generator = () => {
    return JSON.stringify({
      nickname: faker.internet.userName(),
      name: faker.name.findName(),
      email: faker.internet.email(),
      birthday: faker.date.past(10),
      stack: ["c#"],
    });
  };
  http.post(url, Generator(), params);
  sleep(1);
};
