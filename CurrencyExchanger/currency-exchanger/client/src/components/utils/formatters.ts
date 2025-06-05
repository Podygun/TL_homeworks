export function formatNumber(num: number): string {
  return num.toFixed(3);
}

const DATETIME_FORMAT = new Intl.DateTimeFormat('en-GB', {
  weekday: 'short',
  day: '2-digit',
  month: 'short',
  year: 'numeric',
  hour: '2-digit',
  minute: '2-digit',
  hour12: false,
  timeZone: 'UTC'
});

export function formatUpdateTime(date: Date): string {
  const parts = DATETIME_FORMAT.format(date);
  return parts.replace(',', '  ') + ' UTC';
}