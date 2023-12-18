import http from "k6/http";
import { sleep } from "k6";

export const options = {
  scenarios: {
    pre_warmup: {
      executor: "shared-iterations",
      vus: 2,
      iterations: 10,
      startTime: "0s",
    },
    warmup: {
      executor: "shared-iterations",
      vus: 10,
      iterations: 20,
      startTime: "10s",
    },
    stress: {
      executor: "ramping-vus",
      startTime: "20s",
      stages: [
        { duration: "3m", target: 3424 },
        { duration: "1m", target: 3424 },
        { duration: "1m", target: 0 },
      ],
    },
  },
};

export default () => {
  const url = "http://localhost:9999/api/person";
  const params = {
    headers: {
      "Content-Type": "application/json",
    },
  };
  const body = JSON.stringify({
    nickname: "nick123",
    name: "nick",
    email: "nicknick@example.com",
    birthday: "2023-12-07",
    stack: ["c#"],
  });
  http.post(url, body, params);
  sleep(1);
};
