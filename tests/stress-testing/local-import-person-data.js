import faker from "https://cdnjs.cloudflare.com/ajax/libs/Faker/3.1.0/faker.min.js";
import http from "k6/http";
import { sleep } from "k6";

export const options = {
  scenarios: {
    pre_warmup: {
      executor: "shared-iterations",
      vus: 4,
      iterations: 10,
      startTime: "0s",
    },
    warmup: {
      executor: "shared-iterations",
      vus: 8,
      iterations: 20,
      startTime: "10s",
    },
    stress: {
      executor: "ramping-vus",
      startTime: "20s",
      stages: [
        { duration: "3m", target: 500 },
        { duration: "1m", target: 500 },
        { duration: "1m", target: 0 },
      ],
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
